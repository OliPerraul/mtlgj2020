﻿using UnityEngine;
using System.Collections;
using System;


namespace Cirrus
{
    [System.Serializable]
    public class Timer
    {
        [SerializeField]
        bool _repeat = false;

        [SerializeField]
        float _limit = -1;

        [SerializeField]
        float _time = 0f;

        [SerializeField]
        private bool _isFixedUpdate;

        bool active = false;
        public bool IsActive => active;

        public Events.Event<float> OnTickHandler;

        public Events.Event OnTimeLimitHandler;

        private Events.Event OnClockUpdateHandler
        {
            get
            {
                return _isFixedUpdate?
                    Clock.Instance.OnFixedUpdateHandler :
                    Clock.Instance.OnUpdateHandler;
            }

            set {
                if (_isFixedUpdate)
                    Clock.Instance.OnFixedUpdateHandler = value;
                else
                    Clock.Instance.OnUpdateHandler = value;
            }
        }

        public Timer(float limit=1f, bool start = true, bool repeat = false, bool fixedUpdate=false)
        {
            _time = 0;
            _limit = limit;
            _repeat = repeat;
            _isFixedUpdate = fixedUpdate;

            if (start)
            {
                Start();
            }
        }

        public float Time => _time;

        public void Reset(float limit = -1)
        {
            if (limit > 0)
            {
                _limit = limit;
            }

            _time = 0;
        }

        public void Start(float limit = -1)
        {
            Reset(limit);

            if (!active)
            {
                OnClockUpdateHandler += OnTicked;
            }

            active = true;
        }

        public void Resume()
        {
            if (!active)
            {
                OnClockUpdateHandler += OnTicked;
            }

            active = true;
        }

        public void Stop()
        {
            if (active)
            {
                OnClockUpdateHandler -= OnTicked;
            }

            active = false;
        }

        private void OnTicked()
        {
            _time += UnityEngine.Time.deltaTime;
            OnTickHandler?.Invoke(_time);
            if (_time >= _limit)
            {
                _time = _limit;

                OnTimeLimitHandler?.Invoke();

                if (_repeat)
                {
                    Reset();
                }
                else
                {
                    Stop();
                }
            }
        }

        ~Timer()
        {
            Stop();
        }

    }
}
