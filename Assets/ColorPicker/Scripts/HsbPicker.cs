#region Includes
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace TS.ColorPicker
{
    public class HsbPicker : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private ColorSlider _colorSlider;

        [Header("Inner")]
        [SerializeField] private Image _imageColor;

        public delegate void OnValueChanged(HsbPicker sender, float hue, float saturation, float brightness);
        public OnValueChanged ValueChanged;

        public float Hue { get { return _colorSlider.Value; } }
        public float Saturation { get { return _picker.NormalizedValue.x; } }
        public float Brightness { get { return _picker.NormalizedValue.y; } }

        private RectPicker _picker;

        #endregion

        private void Awake()
        {
            _picker = GetComponent<RectPicker>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(_picker == null) { throw new System.Exception("Missing RectPicker"); }
#endif
        }
        private void Start()
        {
            _colorSlider.ValueChanged = Slider_ValueChanged;
            _picker.ValueChanged = RectPicker_ValueChanged;
        }

        public void SetColor(Color color)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out float brightness);

            _colorSlider.Value = hue;
            UpdateImageColor(hue);

            _picker.NormalizedValue = new Vector2(saturation, brightness);
        }

        private void Slider_ValueChanged(ColorSlider sender, float value)
        {
            UpdateImageColor(value);

            InvokeValueChanged();
        }
        private void RectPicker_ValueChanged(RectPicker sender, Vector2 position)
        {
            InvokeValueChanged();
        }

        private void UpdateImageColor(float hue)
        {
            _imageColor.color = Color.HSVToRGB(hue, 1, 1);
        }
        private void InvokeValueChanged()
        {
            ValueChanged?.Invoke(this, Hue, Saturation, Brightness);
        }
    }
}