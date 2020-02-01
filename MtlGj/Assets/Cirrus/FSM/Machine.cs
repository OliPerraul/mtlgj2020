using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Cirrus.FSM
{
    /// <summary>
    /// TODO Handle events do not force everything to appear in the loop
    /// Make it so that some states respond to events
    /// </summary>   

    [System.Serializable]
    public abstract class BaseMachine<A, B> : BaseBehaviour 
        where A : AssetResource 
        where B : SceneResource
    {
        [SerializeField]
        private GameObject _stateLabel;

        [SerializeField]
        [Editor.ObjectSelector]
        public MonoBehaviour[] Context;

        Mutex _mutex;

        private IState _first;

        public override void OnValidate()
        {
            if (_sceneStates.Length == 0)
            {
                _sceneStates = GetComponents<B>();
            }
        }

        public virtual void Add(IState state)
        {
            if (_dictionary.ContainsKey(state.ID))
                return;

            _dictionary.Add(state.ID, state);

            if (_first == null && state.IsStart)
                _first = state;
        }

        public override void Awake()
        {
            _dictionary = new Dictionary<int, IState>();
            _mutex = new Mutex();
            _stack = new Stack<IState>();

            //_dictionary.Add((int)DefaultState.Idle, new Idle.State());
            _first = null;
            foreach (AssetResource res in _assetStates)
            {
                if (res == null) continue;

                if (_dictionary.ContainsKey(res.ID))
                    continue;

                Add(CreateState(res));
            }

            foreach (SceneResource res in _sceneStates)
            {
                if (res == null) continue;

                if (_dictionary.ContainsKey(res.ID))
                    continue;

                Add(CreateState(res));
            }

            if (_first == null)
            {
                if (_dictionary.Count != 0)
                    _first = _dictionary.Values.First();
            }
        }

        public override void Start()
        {
            if (_first == null)
                return;


            TryPushState(_first.ID);
        }


        public Stack<IState> _stack;

        [SerializeField]
        public IState Top
        {
            get
            {
                return _stack == null ?
                    null :
                    _stack.Count == 0 ? null : _stack.Peek();
            }
        }

        [SerializeField]
        public A[] _assetStates;

        [SerializeField]
        public B[] _sceneStates;

        private Dictionary<int, IState> _dictionary;

        private bool _enabled = true;

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }

        public virtual IState CreateState(IResource resource)
        {
            return resource.PopulateState(Context);
        }

        public string StateName = "";

        public virtual void Update()
        {
            if (_stateLabel != null)
            {
                _stateLabel.name = Top == null ? "?" : Top.Name;
            }

            if (!_enabled)
                return;

            if (Top != null)
            {
                Top.BeginUpdate();
                Top.EndUpdate();
            }
        }

        public virtual void FixedUpdate()
        {
            if (!_enabled)
                return;

            if (Top != null)
            {
                Top.BeginUpdate();
                Top.EndUpdate();
            }
        }


        public virtual void OnDrawGizmos()
        {
            if (!_enabled)
                return;



            if (Top != null)
            {
                Top.UpdateDrawGizmos();
            }
        }


        public virtual void DrawGizmosIcon(Vector3 pos)
        {
            if (Top != null)
            {
                //Gizmos.DrawIcon(pos, Top.ToString(), true);
                //Handles.Label(pos, Top.ToString());
                //Utils.TextGizmo.Draw(pos, Top.ToString());

            }
        }

        public virtual bool TrySetState<T>(T state, params object[] args)
        {
            return TrySetState((int)(object)state, args);
        }

        public virtual bool TryPushState<T>(T state, params object[] args)
        {
            return TryPushState((int)(object)state, args);
        }

        public virtual bool TryPushState(int state, params object[] args)
        {
            if (_dictionary.TryGetValue(state, out IState res))
            {
                IState current = Top;

                if (current != null)
                    current.Exit();

                if (current != null && current.ID == res.ID)
                {
                    res.Reenter(args);
                }
                else
                {
                    _mutex.WaitOne();

                    _stack.Push(res);

                    _mutex.ReleaseMutex();

                    res.Enter(args);
                }

                return true;
            }

            return false;
        }


        public virtual bool TryPopState(int from, params object[] args)
        {
            if (_stack.Count > 1)
            {
                if (Top.ID != from)
                    return false;

                return TryPopState();
            }

            return false;
        }

        public virtual bool TryPopState(params object[] args)
        {
            if (_stack.Count > 1)
            {
                Top.Exit();
                IState prev = Top;

                _mutex.WaitOne();
                _stack.Pop();
                _mutex.ReleaseMutex();

                if (prev.ID == Top.ID) Top.Reenter(args);

                else Top.Enter(args);

                return true;
            }

            return false;
        }

        public virtual bool TrySetState(int state, params object[] args)
        {
            if (_dictionary.TryGetValue(state, out IState res))
            {
                IState current = Top;

                if (current != null) current.Exit();

                if (
                    current != null && 
                    current.ID == res.ID)
                {
                    res.Reenter(args);
                }
                else
                {
                    _mutex.WaitOne();

                    _stack.Clear();

                    _stack.Push(res);

                    _mutex.ReleaseMutex();

                    res.Enter(args);
                }

                return true;
            }

            return false;
        }

        public override void OnDestroy()
        {
            foreach (var state in _dictionary)
            {
                if (state.Value == null)
                    continue;

                state.Value.OnMachineDestroyed();
            }
        }
    }

    public abstract class BaseMachine1<A> : BaseMachine<A, SceneResource> where A : AssetResource { }

    public abstract class BaseMachine2<S> : BaseMachine<AssetResource, S> where S : SceneResource { }

    public abstract class BaseMachine : BaseMachine<AssetResource, SceneResource> { }
}
