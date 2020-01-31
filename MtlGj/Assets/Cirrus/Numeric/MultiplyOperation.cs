using UnityEngine;
using System.Collections;
using Cirrus.Editor;
using UnityEditor;

namespace Cirrus.Numeric
{
    [CreateAssetMenu(menuName = "Cirrus/Numeric/Operations/Multiply")]
    public class MultiplyOperation : Operation
    {
        [SerializeField]
        private FlexibleNumber _number;
        
        public override float Evaluate(float current)
        {
            return current * _number.Value;
        }

        public void OnValidate()
        {
            _number.OnValidate();
        }
    }
}