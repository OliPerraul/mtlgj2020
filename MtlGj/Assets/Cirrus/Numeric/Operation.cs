using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cirrus.Numeric
{
    public abstract class Operation : ScriptableObject
    {
        public abstract float Evaluate(float current);

        public virtual float Undo(float current)
        {
            return current - GetDelta(current);
        }

        public virtual float GetDelta(float current)
        {
            return Evaluate(current) - current;
        }
    }
}
