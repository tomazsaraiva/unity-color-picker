#region Includes
using UnityEngine;
#endregion

namespace TS.ColorPicker.Demo
{
    public class ColorPickerDemo : MonoBehaviour
    {
        #region Variables

        private const string PREF_COLOR = "color";

        [Header("References")]
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private ColorPicker _colorPicker;

        private Color _color;
        private Ray _ray;

        #endregion

        private void Start()
        {
            _colorPicker.OnChanged.AddListener(ColorPicker_OnChanged);
            _colorPicker.OnSubmit.AddListener(ColorPicker_OnSubmit);
            _colorPicker.OnCancel.AddListener(ColorPicker_OnCancel);

            var savedColor = PlayerPrefs.GetString(PREF_COLOR, "");
            if(ColorUtility.TryParseHtmlString("#" + savedColor, out _color))
            {
                _renderer.material.color = _color;
            }
            else
            {
                _color = _renderer.material.color;
            }
        }
        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray))
                {
                    _colorPicker.Open(_color);
                }
            }
        }

        private void ColorPicker_OnChanged(Color color)
        {
            _renderer.material.color = color;
        }
        private void ColorPicker_OnSubmit(Color color)
        {
            _color = color;

            PlayerPrefs.SetString(PREF_COLOR, ColorUtility.ToHtmlStringRGBA(_color));
            PlayerPrefs.Save();

        }
        private void ColorPicker_OnCancel()
        {
            _renderer.material.color = _color;
        }
    }
}