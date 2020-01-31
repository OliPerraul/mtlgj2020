using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;

namespace Cirrus.Numeric
{
    [System.Serializable]
    public struct Variable
    {
        public string Name;

        public FlexibleNumber Number;
    }

    // TODO chain operation instead
    [CreateAssetMenu(menuName = "Cirrus/Numeric/Operations/Custom")]
    public class CustomOperation : Operation
    {
        [SerializeField]
        private string _currentVariableName = "CURRENT";

        [SerializeField]
        private List<Variable> _variables;

        [SerializeField]
        private string _expression = "CURRENT + 2";

        [SerializeField]
        private string _resolvedExpression;

        public override float Evaluate(float current)
        {
            string resolved =
            _expression
                .Replace(_currentVariableName, current.ToString());                

            foreach (var variable in _variables)
            {
                resolved = resolved.Replace(variable.Name, variable.Number.Value.ToString());
            }

            return (float)Convert.ToDouble(new DataTable().Compute(resolved, null));
        }
    }
}
