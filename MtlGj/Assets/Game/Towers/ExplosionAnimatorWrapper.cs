using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public enum ExplosionAnimation
    {
        Explosion=-211360833,
    }
    public interface IExplosionAnimatorWrapper
    {
        float GetStateSpeed(ExplosionAnimation state);
        void Play(ExplosionAnimation animation, float normalizedTime);
        void Play(ExplosionAnimation animation);
        float BaseLayerLayerWeight{set;}
    }
    public class ExplosionAnimatorWrapper : IExplosionAnimatorWrapper
    {
        private Animator _animator;
        private Dictionary<ExplosionAnimation,float> _stateSpeedValues = new Dictionary<ExplosionAnimation,float>();
        public void Play(ExplosionAnimation animation, float normalizedTime)
        {
            if(_animator != null)_animator.Play((int)animation, -1, normalizedTime);
        }
        public void Play(ExplosionAnimation animation)
        {
            if(_animator != null)_animator.Play((int)animation);
        }
        public float BaseLayerLayerWeight{set { if(_animator != null) _animator.SetLayerWeight(0,value);} }
        public ExplosionAnimatorWrapper(Animator animator)
        {
            _animator = animator;
            _stateSpeedValues.Add(ExplosionAnimation.Explosion,1f);
        }
        public float GetStateSpeed(ExplosionAnimation state)
        {
            if(_stateSpeedValues.TryGetValue(state, out float res)) return res;
            return -1f;
        }
        public float GetClipLength(ExplosionAnimation state)
        {
            return -1f;
        }
    }
}
