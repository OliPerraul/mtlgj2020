using UnityEngine;
using System.Collections;


namespace Cirrus.HKP.Objects.Characters.Avatar
{
    [System.Serializable]
    public enum EnemyStateID
    {
        Default = 1 << 1,
        Invade = 1 << 2,
        Attack = 1 << 3

    }


    public abstract class EnemyState : FSM.State
    {
        public override int ID => (int)EnemyStateID.Default;

        public Enemy Enemy => (Enemy)_context[0];

        public EnemyState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }


        public override void Enter(params object[] args) { }

        public override void Exit() { }

        public override void BeginUpdate()
        {

        }

        public override void EndUpdate() { }

    }

    public class Attack : EnemyState
    {
        public Attack(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }


    }
    public class Invade : EnemyState
    {
        public Invade(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }


    }



    public class EnemyStateMachine : FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

        [SerializeField]
        private Enemy _enemy;

        public override void Awake()
        {
            base.Awake();

            //Add(State)

            Add(new Invade(true, _enemy));
            Add(new Attack(false, _enemy));
            

            //Add(new Crouched(true, _character));

            //Add(new UsingCamera(false, _character));
        }

        public override void Start()
        {
            base.Start();
        }
    }

}