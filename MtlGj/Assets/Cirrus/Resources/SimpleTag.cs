using UnityEngine;
using System.Collections;

namespace Cirrus.Resources
{
    [CreateAssetMenu(menuName = "Cirrus/Resources/Tag")]
    public class SimpleTag : Tag
    {
        [SerializeField]
        [Editor.EnumFlag]
        public Cirrus.Flag _flags;

        public override int Flags {
            get
            {
                return (int) _flags;
            }
        }
    }
}
