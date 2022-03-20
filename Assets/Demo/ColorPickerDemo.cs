#region Includes
using UnityEngine;
#endregion

namespace TS.ColorPicker.Demo
{
    public class ColorPickerDemo : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private ColorPicker _colorPicker;

        private Color _color;
        private Ray _ray;
        private RaycastHit _hit;

        #endregion

        private void Start()
        {
            _colorPicker.OnChanged.AddListener(ColorPicker_OnChanged);
            _colorPicker.OnSubmit.AddListener(ColorPicker_OnSubmit);
            _colorPicker.OnCancel.AddListener(ColorPicker_OnCancel);

            _color = _renderer.material.color;
        }
        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 100))
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
        }
        private void ColorPicker_OnCancel()
        {
            _renderer.material.color = _color;
        }
    }
}