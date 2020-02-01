using UnityEngine;
using System.Collections;

using Pathfinding = NesScripts.Controls.PathFind;
using NesScripts.Controls.PathFind;
using Cirrus.Extensions;
using System.Collections.Generic;

namespace MTLGJ
{
    [System.Serializable]
    public enum TowerStateID : int
    {
        Active,
        Idle,
        Broken1,
        Broken2,
        Broken3,
        Explode
    }

    public abstract class TowerState : Cirrus.FSM.State
    {
        public override int ID => -1;

        public abstract Tower Tower { get; }

        public TowerState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }
    }

    public class TowerIdle : TowerState
    {
        public override int ID => (int)TowerStateID.Idle;

        public override Tower Tower => (Tower) _context[0];

        public TowerIdle(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }
    }


    public class TowerStateMachine : Cirrus.FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

        public override void Awake()
        {
            base.Awake();

            //Add(new StartSession(true, this));
            //Add(new StartRound(false, this));
            //Add(new InRound(false, this));
            //Add(new Intermission(false, this));
        }

        public override void Start()
        {
            base.Start();

        }
    }

}