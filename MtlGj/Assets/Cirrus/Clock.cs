using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cirrus
{
    public class Clock : BaseSingleton<Clock>
    {
        public Events.Event OnUpdateHandler;

        public Events.Event OnFixedUpdateHandler;

        public void Update()
        {
            OnUpdateHandler?.Invoke();
        }

        public void FixedUpdate()
        {
            OnFixedUpdateHandler?.Invoke();
        }
    }
}
