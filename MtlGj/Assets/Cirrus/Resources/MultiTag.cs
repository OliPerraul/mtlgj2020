using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cirrus.Resources
{
    [CreateAssetMenu(menuName="Cirrus/Resources/MultiTag")]
    public class MultiTag : Tag
    {
        private int _flags;

        public override int Flags
        {
            get {
                return _flags;
            }
        }

        [SerializeField]
        private List<Tag> _tags;

        public void Awake()
        {
            foreach (var tag in _tags)
            {
                if (tag == null) continue;

                _flags |= tag.Flags;
            }
        }



    }
}
