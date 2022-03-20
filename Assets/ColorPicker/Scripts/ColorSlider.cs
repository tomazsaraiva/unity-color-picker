#region Includes
using System;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace TS.ColorPicker
{
    [RequireComponent(typeof(PointerTrackerArea), typeof(Slider))]
    public class ColorSlider : MonoBehaviour
    {
        #region Variables

        public delegate void OnValueChanged(ColorSlider sender, float value);
        public OnValueChanged ValueChanged;

        public float Value
        {
            get { return _slider.value; }
            set { _slider.SetValueWithoutNotify(value); }
        }

        private Slider _slider;
        private PointerTrackerArea _tracker;

        #endregion

        private void Awake()
        {
            _slider = GetComponent<Slider>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(_slider == null) { throw new Exception("Missing Slider"); }
#endif

            _tracker = GetComponent<PointerTrackerArea>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_tracker == null) { throw new Exception("Missing PointerTrackerArea"); }
#endif

        }
        private void Start()
        {
            _slider.onValueChanged.AddListener(Slider_ValueChanged);
            _tracker.Drag = PointerTrackerArea_Drag;
        }

        private void Slider_ValueChanged(float arg0)
        {
            ValueChanged?.Invoke(this, arg0);
        }
        private void PointerTrackerArea_Drag(PointerTrackerArea sender, Vector2 position)
        {
            _slider.value = sender.Normalize(position).y;
        }
    }
}