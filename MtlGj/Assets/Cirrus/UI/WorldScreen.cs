////using Cirrus.DungeonHealer.World.Cameras;
//using Cirrus.Extensions;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Cirrus.UI
//{
//    public class WorldScreen : BaseBehaviour
//    {
//        [SerializeField]
//        private Transform _anchor;

//        private Vector3 _target;

//        private Vector3 _candidate;

//        [SerializeField]
//        private Vector3 _offset = Vector3.zero;


//        //[SerializeField]
//        //private CameraWrapper _camera;

//        [SerializeField]
//        private float _smooth = 0.9f;


//        [SerializeField]
//        private RectTransform rect;

//        private void Awake()
//        {
//            rect = GetComponent<RectTransform>();
//        }

//        public void OnValidate()
//        {
//            if (_camera == null)
//            {
//                _camera = FindObjectOfType<CameraWrapper>();
//            }
//        }

//        void LateUpdate()
//        {
//            if (_camera != null)
//            {
//                _candidate.x = UnityEngine.Mathf.Round(_anchor.position.x * 100f) / 100f;
//                _candidate.y = UnityEngine.Mathf.Round(_anchor.position.y * 100f) / 100f;
//                _candidate.z = UnityEngine.Mathf.Round(_anchor.position.z * 100f) / 100f;
//                _candidate = _camera.Camera.WorldToScreenPoint(_candidate);
//                _candidate = _candidate.Round(); // Pixel snapping
//                _candidate.z = 0;
//                rect.position = _candidate + _offset;
//            }
//        }
//    }
//}



