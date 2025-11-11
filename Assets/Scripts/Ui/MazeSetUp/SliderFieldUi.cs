using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Maze.Ui.MazeSetUp
{
    public class SliderFieldUi: MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI sliderValueText;
        [SerializeField] private float step = 0.1f;
        [SerializeField] private bool useStep = false;
        
        public float Value => slider.value;
        public int ValueInt => (int)slider.value;
        
        private void Awake()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
            sliderValueText.text = Value.ToString(CultureInfo.InvariantCulture);
        }

        private void OnValueChanged(float value)
        {
            if (useStep && !slider.wholeNumbers)
            {
                value = ApplyStep(value);
            }
            sliderValueText.text = value.ToString(CultureInfo.InvariantCulture);
        }

        public void SetLimits(float minValue, float maxValue)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = Mathf.Clamp(slider.value, minValue, maxValue);
        }
        
        private float ApplyStep(float value)
        {
            if (step > 0)
            {
                value = Mathf.Round(value / step) * step;
                slider.SetValueWithoutNotify(value); 
            }
            return value;
        }
    }
}