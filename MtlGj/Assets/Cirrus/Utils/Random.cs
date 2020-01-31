using UnityEngine;
using System.Collections;

namespace Cirrus.Utils
{
    public static class Random
    {
        public static bool Boolean => UnityEngine.Random.value > 0.5f;
    }
}
