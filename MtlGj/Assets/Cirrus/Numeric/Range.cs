using UnityEngine;
using System.Collections;

namespace Cirrus.Numeric
{

    public interface IRange : INumber
    {
        float Min { get; }

        float Max { get; }

    }


    [System.Serializable]
    public class RangeInt : IRange
    {
        [SerializeField]
        private int _min;

        [SerializeField]
        private int _max;

        public float Value => Random.Range(_min, _max);

        public float Max => _max;

        public float Min => _min;

        public RangeInt(int min, int max)
        {
            _min = min;
            _max = max;
        }
    }

    [System.Serializable]
    public class RangeFloat : IRange
    {
        [SerializeField]
        private float _min;

        public float Min => _min;

        [SerializeField]
        private float _max;

        public float Max => _max;

        public float Value => Random.Range(_min, _max);

        public RangeFloat(float min=0, float max=1)
        {
            _min = min;
            _max = max;
        }
    }

    [System.Serializable]
    public class Range : IRange
    {
        [SerializeField]
        private SimpleNumber _min;

        [SerializeField]
        private SimpleNumber _max;

        public float Value => Random.Range(_min.Value, _max.Value);

        public float Min => _min.Value;

        public float Max => _max.Value;
    }
}