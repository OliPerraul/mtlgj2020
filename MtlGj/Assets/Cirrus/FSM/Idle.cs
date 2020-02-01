using UnityEngine;
using UnityEditor;

namespace Cirrus.FSM
{
    public class Idle : AssetResource
    {
        override public int ID { get { return (int) DefaultState.Idle; } }

        public override FSM.IState PopulateState(object[] context)
        {
            throw new System.NotImplementedException();
        }

        new public class State : FSM.ResourceState
        {
            public State() : base(null, null) { }
        }
    }

}
