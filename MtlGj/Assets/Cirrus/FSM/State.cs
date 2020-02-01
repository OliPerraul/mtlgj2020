using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Cirrus.FSM
{
    enum DefaultState
    {
        Idle = -1,
    }

    public interface IResource
    {
        int ID { get ; }

        IState PopulateState(object[] context);

        string Name { get; }

        bool IsStart { get; }
    }


    public interface IState
    {
        string Name { get; }

        bool IsStart { get; }

        int ID { get; }// { return resource.Id; } }

        void Enter(params object[] args);

        void Exit();

        void Reenter(params object[] args);

        void BeginUpdate();

        void EndUpdate();

        void UpdateDrawGizmos();

        void OnMachineDestroyed();                
    }

    public abstract class AssetResource : ScriptableObject, IResource
    {
        virtual public int ID { get { return -1; } }
        public abstract IState PopulateState(object[] context);
        
        [SerializeField]
        private bool _isStart = false;

        public string Name => name;

        public bool IsStart { get { return _isStart;  } }
    }

    public abstract class ResourceState : IState
    {
        public IResource Resource;
        public object[] Context;

        public int ID { get { return Resource.ID; } }

        public ResourceState(IResource resource, params object[] context)
        {
            this.Resource = resource;
            this.Context = context;
        }

        public virtual string Name => Resource.Name;

        public virtual bool IsStart => Resource.IsStart;

        public virtual void Enter(params object[] args) { }

        public virtual void Exit() { }

        public virtual void Reenter(params object[] args) { }

        public virtual void BeginUpdate() { }

        public virtual void EndUpdate() { }

        public virtual void BeginFixedUpdate() { }

        public virtual void EndFixedUpdate() { }

        public virtual void UpdateDrawGizmos() { }

        public virtual void OnMachineDestroyed()
        {
            
        }
    }

    [System.Serializable]
    public abstract class State : IState
    {
        public virtual int ID => -1;

        public virtual string Name => "[?]";

        private bool _isStart = false;

        protected object[] _context;

        public virtual bool IsStart => _isStart;

        public State(bool isStart, params object[] context) {

            _isStart = isStart;

            _context = context;
        }

        public virtual void Enter(params object[] args) { }

        public virtual void Exit() { }

        public virtual void Reenter(params object[] args) { }

        public virtual void BeginUpdate() { }

        public virtual void EndUpdate() { }

        public virtual void UpdateDrawGizmos() { }

        public virtual void OnMachineDestroyed()
        {

        }
    }

    public abstract class SceneResource : BaseBehaviour, IResource, IState
    {
        public virtual string Name => name;

        [SerializeField]
        private bool _isStart = false;

        public bool IsStart { get { return _isStart; } }

        public virtual int ID { get { return -1; } }

        public abstract IState PopulateState(object[] context);

        public virtual void Enter(params object[] args) { }

        public virtual void Exit() { }

        public virtual void Reenter(params object[] args) { }

        public virtual void BeginUpdate() { }

        public virtual void EndUpdate() { }

        public virtual void BeginFixedUpdate() { }

        public virtual void EndFixedUpdate() { }

        public virtual void UpdateDrawGizmos() { }

        public virtual void OnMachineDestroyed() { }
    }



}
