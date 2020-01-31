using UnityEngine;
using System.Collections;
using Cirrus.Editor;
using UnityEditor;

namespace Cirrus.Numeric
{
    [CreateAssetMenu(menuName = "Cirrus/Numeric/Operations/Undo")]
    public class UndoOperation : Operation
    {
        [SerializeField]
        private Operation _operation;

        public override float Evaluate(float current)
        {
            return _operation.Undo(current);
        }

        public override float Undo(float current)
        {
            // DO NOT UNDO AN UNDO!!
            Debug.Assert(false, "Undo operation cannot be undone; it's meaningless!");
            return 0;
        }
               
    }
}