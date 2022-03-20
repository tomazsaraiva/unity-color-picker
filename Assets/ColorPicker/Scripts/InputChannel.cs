#region Includes
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace TS.ColorPicker
{
    public class InputChannel : MonoBehaviour
    {
        #region Variables

        [Header("Inner")]
        [SerializeField] private Text _text;
        [SerializeField] private InputField _input;

        public delegate void OnValueChanged(InputChannel sender, float value, int value32);
        public OnValueChanged ValueChanged;

        public string Label
        {
            get { return _text.text; }
            set { _text.text = value; }
        }
        public float Value
        {
            get { return Value32 / 255f; }
            set { Value32 = Mathf.RoundToInt(value * 255f); }
        }
        public int Value32
        {
            get { return Mathf.Clamp(int.Parse(_input.text), 0, 255); }
            set { _input.text = Mathf.Clamp(value, 0, 255).ToString(); }
        }

        #endregion

        private void Start()
        {
            _input.onEndEdit.AddListener(Input_EndEdit);
        }

        private void Input_EndEdit(string arg0)
        {
            if (string.IsNullOrEmpty(arg0)) { return; }

            ValueChanged?.Invoke(this, Value, Value32);
        }
    }
}