//using UnityEngine;
//using System.Collections;


//namespace Cirrus.HKP.Objects.Characters.Avatar
//{
//    [System.Serializable]
//    public enum StateId
//    {
//        Default = 1 << 1,
//        UsingCamera = 1 << 2,
//        Standing = 1 << 3,
//        Crouching = 1 << 4,
//    }


//    public abstract class State : FSM.State
//    {
//        public override int Id => (int)StateId.Default;

//        public State(
//            bool isStart,
//            object[] context) : base(
//            isStart,
//            context)
//        {
//            _useCameraTimer = new Timer(
//            //Avatar.UseCameraTime,
//            repeat: false,
//            start: false);
//        }

//        //protected Avatar Avatar => (Avatar)_context[0];

//        protected Timer _useCameraTimer;

//        public override void Enter(params object[] args) { }

//        public override void Exit() { }

//        public override void BeginUpdate()
//        {

//        }

//        public override void EndUpdate() { }

//    }

//    public abstract class Locomotion : State
//    {
//        public Locomotion(bool isStart, params object[] context) : base(isStart, context) { }

//        public override void BeginUpdate()
//        {
//            base.BeginUpdate();

//            //// Left, not right
//            //if (
//            //    Avatar.InputScheme.IsLeftHeld &&
//            //    !Avatar.InputScheme.IsRightHeld)
//            //{
//            //    Avatar.AccelerationDecelerationTime = Avatar.Direction != -1 ? 0 : Avatar.AccelerationDecelerationTime;
//            //    Avatar.Direction = -1;
//            //    Avatar.Speed = Mathf.Lerp(Avatar.Speed, -Avatar.MaxSpeed, Avatar.AccelerationDecelerationTime / Avatar.AccelerationTimeLimit);
//            //}
//            //// Not left, right
//            //else if (
//            //    !Avatar.InputScheme.IsLeftHeld &&
//            //    Avatar.InputScheme.IsRightHeld)
//            //{
//            //    Avatar.AccelerationDecelerationTime = Avatar.Direction != 1 ? 0 : Avatar.AccelerationDecelerationTime;
//            //    Avatar.Direction = 1;
//            //    Avatar.Speed = Mathf.Lerp(Avatar.Speed, Avatar.MaxSpeed, Avatar.AccelerationDecelerationTime / Avatar.AccelerationTimeLimit);
//            //}
//            //else
//            //{
//            //    Avatar.AccelerationDecelerationTime = Avatar.Direction != 0 ? 0 : Avatar.AccelerationDecelerationTime;
//            //    Avatar.Direction = 0;
//            //    Avatar.Speed = Mathf.Lerp(Avatar.Speed, 0, Avatar.DecelerationTimeLimit / Avatar.DecelerationTimeLimit);
//            //}

//            //Avatar.AccelerationDecelerationTime += Time.deltaTime;

//            //Avatar.Transform.position += Vector3.right * Avatar.Speed;
//        }
//    }

//    public class Crouched : Locomotion
//    {
//        public Crouched(bool isStart, params object[] context) : base(isStart, context) { }

//        public override void BeginUpdate()
//        {
//            base.BeginUpdate();

//            //if (Avatar.InputScheme.IsUpHeld)
//            //{
//            //    Avatar.StateMachine.TrySetState(StateId.Standing);
//            //    return;
//            //}

//        }
//    }



//    public class Standing : Locomotion
//    {
//        public override int Id => (int)StateId.Standing;

//        public override string Name => "Avatar.Standing";

//        public Standing(
//            bool isStart,
//            params object[] context) : base(isStart, context) { }


//        public override void Enter(params object[] args)
//        {
//            base.Enter(args);

//            //Cameras.CameraWrapper.Instance.State.Value = Cameras.CameraState.Idle;

//            //_useCameraTimer.Start();
//        }

//        public override void Reenter(params object[] args)
//        {
//            base.Reenter(args);

//            //Cameras.CameraWrapper.Instance.State.Value = Cameras.CameraState.Idle;
//        }

//        public override void BeginUpdate()
//        {
//            base.BeginUpdate();

//            //if (!_useCameraTimer.IsActive &&
//            //    Avatar.InputScheme.IsCameraPressed)
//            //{
//            //    Avatar.StateMachine.TrySetState(StateId.UsingCamera);
//            //    return;
//            //}

//            //if (Avatar.InputScheme.IsDownPressed)
//            //{
//            //    Avatar.StateMachine.TrySetState(StateId.Crouching);
//            //    return;
//            //}
//        }

//        public override void EndUpdate()
//        {
//            base.EndUpdate();

//            //if (Avatar.Direction == 0)
//            //    Avatar.AnimatorWrapper.Play(AvatarAnimation.Idle_Side);

//            //else Avatar.AnimatorWrapper.Play(AvatarAnimation.Walk_Side);
//        }
//    }

//    public class UsingCamera : State
//    {
//        public override int Id => (int)StateId.UsingCamera;

//        public override string Name => "Avatar.UsingCamera";

//        public UsingCamera(bool isStart, params object[] context) : base(isStart, context) { }

//        public override void Enter(params object[] args)
//        {
//            base.Enter(args);

//            //Cameras.CameraWrapper.Instance.State.Value = Cameras.CameraState.HandHeld;

//            _useCameraTimer.Start();
//        }

//        public override void Reenter(params object[] args)
//        {
//            base.Reenter(args);

//            //Avatar.AnimatorWrapper.Play(AvatarAnimation.Idle_Back);

//            //Cameras.CameraWrapper.Instance.State.Value = Cameras.CameraState.HandHeld;
//        }

//        public override void BeginUpdate()
//        {
//            base.BeginUpdate();

//            //if (!_useCameraTimer.IsActive &&
//            //    Avatar.InputScheme.IsCameraPressed)
//            //{
//            //    Avatar.StateMachine.TrySetState(StateId.Standing);
//            //    return;
//            //}
//        }
//    }

//    public class AIStateMachine : FSM.BaseMachine
//    {
//        //[SerializeField]
//        //private Avatar _character;

//        public override void Awake()
//        {
//            base.Awake();

//            //Add(new Standing(true, _character));

//            //Add(new Crouched(true, _character));

//            //Add(new UsingCamera(false, _character));
//        }

//        public override void Start()
//        {
//            base.Start();
//        }
//    }

//}