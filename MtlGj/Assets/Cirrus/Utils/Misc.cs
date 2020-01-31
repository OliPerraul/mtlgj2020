using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cirrus.Utils
{


    public class Validation
    {
        public static void Error(string err)
        {
            Debug.LogError(err);
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();

        }

        public static void Assert(bool condition, string msg = "")
        {
#if UNITY_EDITOR

            if (!condition)
            {
                Debug.LogError("Assertion failed " + msg);
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }

#endif
        }


        public static void AssertNotNull(object obj, string idname = "<unknown-identifier>", string classname = "<unknown-identifier>")
        {
#if UNITY_EDITOR

            if (obj == null)
            {
                Debug.LogError("Missing reference '" + idname + "' in the inspector for class '" + classname + "'.");
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }

#endif
        }


    }

    public class GameObjects
    {
        private static void _RecurseCollapseChildrenToList<TComponent>(GameObject parent, ref List<TComponent> collapsedChildren)
        {
            foreach (Transform child in parent.transform)
            {
                var component = child.gameObject.GetComponent<TComponent>();
                if (component != null)
                {
                    collapsedChildren.Add(component);
                }

                _RecurseCollapseChildrenToList(child.gameObject, ref collapsedChildren);
            }
        }

        public static List<TComponent> CollapseChildrenToList<TComponent>(GameObject parent)
        {
            List<TComponent> collapsedChildren = new List<TComponent>();

            foreach (Transform child in parent.transform)
            {
                _RecurseCollapseChildrenToList(child.gameObject, ref collapsedChildren);
            }

            return collapsedChildren;
        }
    }


    public class Mathf
    {
        public static float EaseSinInOut(float lerp, float start, float change)
        {
            return -change / 2 * (UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * lerp) - 1) + start;
        }

        private const float tolerance = 0.1f;
        public static bool CloseEnough(float a, float b, float tolerance = Mathf.tolerance)
        {
            return (UnityEngine.Mathf.Abs(a - b) < tolerance);
        }

        public static float Normalize(float value, float min, float max, float zero)
        {
            if (zero < min)
                zero = min;
            // Prevent NaN/Inf from dividing 0 by something.
            if (UnityEngine.Mathf.Approximately(value, min))
            {
                if (min < zero)
                    return -1f;
                return 0f;
            }
            var percentage = (value - min) / (max - min);
            if (min < zero)
                return 2 * percentage - 1;
            return percentage;
        }

        public static float DeadZone(float value, float min, float max)
        {
            var absValue = UnityEngine.Mathf.Abs(value);
            if (absValue < min)
                return 0;
            if (absValue > max)
                return UnityEngine.Mathf.Sign(value);

            return UnityEngine.Mathf.Sign(value) * ((absValue - min) / (max - min));
        }


    }
}
