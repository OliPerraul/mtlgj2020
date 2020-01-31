using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Cirrus.UI
{
    public static class Utils
    {
        public static bool IsUIElementActive
        {
            get
            {
                var eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                return results.Count > 0;
            }
        }
    }
}