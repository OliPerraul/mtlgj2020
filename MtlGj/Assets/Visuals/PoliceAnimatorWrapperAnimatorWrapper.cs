using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public enum PoliceAnimatorWrapperAnimation
    {
        Anim_Walk=-161352004,
        Anim_Walk2=-655089572,
    }
    public interface IPoliceAnimatorWrapperAnimatorWrapper
    {
        float GetStateSpeed(PoliceAnimatorWrapperAnimation state);
        void Play(PoliceAnimatorWrapperAnimation animation, float normalizedTime);
        void Play(PoliceAnimatorWrapperAnimation animation);
        float BaseLayerLayerWeight{set;}
    }
    public class PoliceAnimatorWrapperAnimatorWrapper : IPoliceAnimatorWrapperAnimatorWrapper
    {
        private Animator _animator;
        private Dictionary<PoliceAnimatorWrapperAnimation,float> _stateSpeedValues = new Dictionary<PoliceAnimatorWrapperAnimation,float>();
        public void Play(PoliceAnimatorWrapperAnimation animation, float normalizedTime)
        {
            if(_animator != null)_animator.Play((int)animation, -1, normalizedTime);
        }
        public void Play(PoliceAnimatorWrapperAnimation animation)
        {
            if(_animator != null)_animator.Play((int)animation);
        }
        public float BaseLayerLayerWeight{set { if(_animator != null) _animator.SetLayerWeight(0,value);} }
        public PoliceAnimatorWrapperAnimatorWrapper(Animator animator)
        {
            _animator = animator;
            _stateSpeedValues.Add(PoliceAnimatorWrapperAnimation.Anim_Walk,1f);
            _stateSpeedValues.Add(PoliceAnimatorWrapperAnimation.Anim_Walk2,1f);
        }
        public float GetStateSpeed(PoliceAnimatorWrapperAnimation state)
        {
            if(_stateSpeedValues.TryGetValue(state, out float res)) return res;
            return -1f;
        }
        public float GetClipLength(PoliceAnimatorWrapperAnimation state)
        {
            return -1f;
        }
    }
}
