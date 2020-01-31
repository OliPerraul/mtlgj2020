using UnityEngine;
using System.Collections;
namespace Cirrus.Extensions
{
    public static class FloatExtension
    {
        public static bool IsCloseEnough(this float val, float other, float tolerance = 0.1f)
        {
            return UnityEngine.Mathf.Abs(val - other) < tolerance;
        }
    }
}