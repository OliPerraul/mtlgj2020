using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public enum PoliceAnimation
    {
        Anim_Walk=-161352004,
        Anim_Walk2=-655089572,
    }
    public interface IPoliceAnimatorWrapper
    {
        float GetStateSpeed(PoliceAnimation state);
        void Play(PoliceAnimation animation, float normalizedTime);
        void Play(PoliceAnimation animation);
        float BaseLayerLayerWeight{set;}
    }
    public class PoliceAnimatorWrapper : IPoliceAnimatorWrapper
    {
        private Animator _animator;
        private Dictionary<PoliceAnimation,float> _stateSpeedValues = new Dictionary<PoliceAnimation,float>();
        public void Play(PoliceAnimation animation, float normalizedTime)
        {
            if(_animator != null)_animator.Play((int)animation, -1, normalizedTime);
        }
        public void Play(PoliceAnimation animation)
        {
            if(_animator != null)_animator.Play((int)animation);
        }
        public float BaseLayerLayerWeight{set { if(_animator != null) _animator.SetLayerWeight(0,value);} }
        public PoliceAnimatorWrapper(Animator animator)
        {
            _animator = animator;
            _stateSpeedValues.Add(PoliceAnimation.Anim_Walk,1f);
            _stateSpeedValues.Add(PoliceAnimation.Anim_Walk2,1f);
        }
        public float GetStateSpeed(PoliceAnimation state)
        {
            if(_stateSpeedValues.TryGetValue(state, out float res)) return res;
            return -1f;
        }
        public float GetClipLength(PoliceAnimation state)
        {
            return -1f;
        }
    }
}
