using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cirrus.Visuals
{

    public class MeshRendererHelper : BaseBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void OnValidate()
        {
            if (_meshRenderer != null && _meshRenderer.sharedMaterials.Length == 0)
            {
                //_meshRenderer.sharedMaterials = GetComponents<Material>();
            }
        }
    }
}