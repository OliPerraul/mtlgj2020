using UnityEngine;
using System.Collections;

namespace Cirrus.Resources
{
    [CreateAssetMenu(menuName = "Cirrus/Resources/String")]
    public class String : ScriptableObject
    {
        [SerializeField]
        private string _value = "[?]";

        public string Value => _value;
    }
}