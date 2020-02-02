using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        
        public ExplosionAnimatorWrapper _wrapper;

        public void Awake()
        {
            _wrapper.Play(ExplosionAnimation.Explosion);
        }    
    }
}
