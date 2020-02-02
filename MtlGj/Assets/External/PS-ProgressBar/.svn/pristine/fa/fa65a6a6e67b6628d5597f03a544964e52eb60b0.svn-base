using UnityEngine;
using System.Collections;
using PlayfulSystems;
using PlayfulSystems.ProgressBar;

namespace PlayfulSystems.ProgressBar
{
    [ExecuteInEditMode]
    public class ProgressBarPro : MonoBehaviour
    {

        public enum AnimationType { FixedTimeForChange, ChangeSpeed }

        [SerializeField]
        [Range(0f, 1f)]
        private float valuePercentage = 1f;
        private float displayValuePercentage = -1f;


        // CUSTOM

        private float value = 1f;
        private float displayValue = -1f;

        private float maxValue = 1f;
        private float displayMaxValue = -1f;


        [Space(10)]
        [Tooltip("Smoothes out the animation of the bar.")]
        [SerializeField]
        bool animateBar = true;

        public bool IsBarAnimated
        {
            get => animateBar;
            set => animateBar = value;
        }

        [SerializeField]
        AnimationType animationType = AnimationType.FixedTimeForChange;
        [SerializeField]
        float animTime = .25f;

        [SerializeField]
        private float minAnimDelta = 1f;

        [Space(10)]
        [SerializeField]
        ProgressBarProView[] views;        

        public void Start()
        {
            if (views == null || views.Length == 0)
                views = GetComponentsInChildren<ProgressBarProView>();
        }

        void OnEnable()
        {
            SetDisplayValue(valuePercentage, true);
        }

        // Public Methods 

        public float Value
        {
            get
            {
                return valuePercentage;
            }

            set
            {
                if (value == valuePercentage)
                    return;

                SetValue(value);
            }
        }

        public void SetValue(float value, float maxValue)
        {
            if (maxValue != 0f)
                SetValue(value / maxValue);
            else
                SetValue(0f);

            // Custom
            this.value = value;
            this.maxValue = maxValue;

            for (int i = 0; i < views.Length; i++)
            {
                if (views[i] == null)
                    continue;

                views[i].NewChangeStarted(
                    displayValue, 
                    value, 
                    displayMaxValue, 
                    maxValue);
            }

            float delta = Mathf.Abs(displayValue - value);
            float deltaMax = Mathf.Abs(displayMaxValue - maxValue);

            if (animateBar && 
                Application.isPlaying && 
                gameObject.activeInHierarchy &&
                (delta > minAnimDelta || deltaMax > minAnimDelta))
            {
                StartSizeAnim(
                    value,
                    maxValue);
            }
            else
            {
                SetDisplayValue(
                    value,
                    maxValue);
            }
        }

        public void SetValue(int value, int maxValue)
        {
            if (value != 0)
                SetValue((float)value, (float)maxValue);
            else
                SetValue(0f, (float)maxValue);
        }

        public void SetValue(float percentage)
        {
            if (Mathf.Approximately(valuePercentage, percentage))
                return;

            valuePercentage = Mathf.Clamp01(percentage);

            for (int i = 0; i < views.Length; i++)
            {
                if (views[i] == null)
                    continue;

                views[i].NewChangeStarted(
                    displayValuePercentage, 
                    valuePercentage);
            }

            float delta = Mathf.Abs(valuePercentage - displayValuePercentage);                        

            if (animateBar && 
                Application.isPlaying && 
                gameObject.activeInHierarchy &&
                (delta > minAnimDelta))
            {
                StartSizeAnim(valuePercentage);
            }
            else
            {
                SetDisplayValue(valuePercentage);
            }
        }

        public bool IsAnimating()
        {
            if (animateBar == false)
                return false;

            return !Mathf.Approximately(displayValuePercentage, valuePercentage);
        }

        // COLOR SETTINGS

        public void SetBarColor(Color color)
        {
            for (int i = 0; i < views.Length; i++)
                views[i].SetBarColor(color);
        }

        // SIZE ANIMATION

        private Coroutine sizeAnim;

        void StartSizeAnim(float percentage)
        {
            if (sizeAnim != null)
                StopCoroutine(sizeAnim);

            sizeAnim = StartCoroutine(DoBarSizeAnim());
        }

        IEnumerator DoBarSizeAnim()
        {
            float startValue = displayValuePercentage;

            float time = 0f;

            float change = valuePercentage - displayValuePercentage;

            float duration = (animationType == AnimationType.FixedTimeForChange ? animTime : Mathf.Abs(change) / animTime);

            while (time < duration)
            {
                time += Time.deltaTime;
                SetDisplayValue(Utils.EaseSinInOut(time / duration, startValue, change));
                yield return null;
            }

            SetDisplayValue(valuePercentage, true);

            sizeAnim = null;
        }

        private Coroutine customSizeAnim = null;

        void StartSizeAnim(float value, float maxValue)
        {
            if (customSizeAnim != null)
            {
                StopCoroutine(customSizeAnim);
            }

            customSizeAnim = StartCoroutine(DoCustomBarSizeAnim());
        }

        IEnumerator DoCustomBarSizeAnim()
        {
            float startValue = displayValue;

            float startMaxValue = displayMaxValue;

            float time = 0f;

            float valueChange = value - displayValue;

            float maxValueChange = maxValue - displayMaxValue;

            float duration = 
                (animationType == AnimationType.FixedTimeForChange ? 
                    animTime : 
                        Mathf.Abs(valueChange) / animTime);

            while (time < duration)
            {
                time += Time.deltaTime;
                SetDisplayValue(
                    Utils.EaseSinInOut(time / duration, startValue, valueChange),
                    Utils.EaseSinInOut(time / duration, startMaxValue, maxValueChange)
                    );
                yield return null;
            }

            SetDisplayValue(value, maxValue, true);

            customSizeAnim = null;
        }



        // Set Value & Update Views

        void SetDisplayValue(
            float value, 
            float maxValue,
            bool forceUpdate = false)
        {
            // If the value hasn't changed don't update any views.
            if (!forceUpdate && 
                displayValuePercentage >= 0f && 
                Mathf.Approximately(displayValuePercentage, value))
                    return;

            displayValue = value;
            displayMaxValue = maxValue;

            UpdateBarViews(
                displayValue, 
                value, 
                displayMaxValue, 
                maxValue, 
                forceUpdate);
        }

        void SetDisplayValue(float value, bool forceUpdate = false)
        {
            // If the value hasn't changed don't update any views.
            if (!forceUpdate && displayValuePercentage >= 0f && Mathf.Approximately(displayValuePercentage, value))
                return;

            displayValuePercentage = value;

            UpdateBarViews(
                displayValuePercentage, 
                valuePercentage, 
                forceUpdate);
        }

        void UpdateBarViews(
            float displayValue, 
            float value,
            float displayMaxValue,
            float maxValue,
            bool forceUpdate = false)
        {
            if (views != null)
            {
                for (int i = 0; i < views.Length; i++)
                {
                    if (views[i] != null)
                    {
                        if (
                            forceUpdate ||
                            views[i].CanUpdateView(
                                displayValue, 
                                value, 
                                displayMaxValue, 
                                maxValue))
                        {
                            views[i].UpdateView(
                                displayValue,
                                value,
                                displayMaxValue,
                                maxValue);
                        }
                    }
                }
            }
        }


        void UpdateBarViews(
            float currentValue, 
            float targetValue, 
            bool forceUpdate = false)
        {
            if (views != null)
                for (int i = 0; i < views.Length; i++)
                    if (views[i] != null)
                        if (forceUpdate || views[i].CanUpdateView(currentValue, targetValue))
                            views[i].UpdateView(currentValue, targetValue);
        }

        // Update Bar in editor

#if UNITY_EDITOR
        private void OnValidate()
        {
            valuePercentage = Mathf.Clamp01(valuePercentage);

            // This is to also display shadows in editor
            if (valuePercentage >= 1f)
                UpdateBarViews(valuePercentage, 0.75f);
            else
                UpdateBarViews(valuePercentage, valuePercentage + (1 - valuePercentage) / 2f);
        }

        private void Reset()
        {
            DetectViewObjects();
        }

        public void AddView(ProgressBarProView view)
        {
            DetectViewObjects();
        }

        public void DetectViewObjects()
        {
            views = GetComponentsInChildren<ProgressBarProView>(true);
        }
#endif
    }
}