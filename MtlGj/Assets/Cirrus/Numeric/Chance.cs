using UnityEngine;
using System.Collections;

namespace Cirrus.Numeric
{
    [System.Serializable]
    public class Chance
    {
        [SerializeField]
        [Range(0f, 1f)]
        public float Probability;

        public Chance(
            float probability)
        {
            Probability = probability;
        }

        public bool IsTrue
        {
            get
            {
                return Random.Range(0f, 1f) <= Probability;
            }
        }

        public static bool CheckIsTrue(float prob)
        {
            return Random.Range(0f, 1f) <= prob;
        }

    }
}
