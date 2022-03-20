#region Includes
using System;
using UnityEngine;
#endregion

namespace TS.ColorPicker
{
    public class InputColorChannels : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private InputChannel[] _inputs;

        public delegate void OnValueChanged(InputColorChannels sender, Color color);
        public OnValueChanged ValueChanged;

        #endregion

        private void Start()
        {
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i].ValueChanged = InputChannel_ValueChanged;
            }
        }

        public void SetValues(float[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                _inputs[i].Value = values[i];
            }
        }

        private void InputChannel_ValueChanged(InputChannel sender, float value, int value32)
        {
            ValueChanged?.Invoke(this, new Color(_inputs[0].Value, _inputs[1].Value, _inputs[2].Value));
        }
    }
}