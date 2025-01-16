using UnityEngine;
using UnityEngine.UI;

namespace DT.UI.Core
{
    public class ProgressionBarR : MonoBehaviour
    {
        public float currentValue, minimumValue, maximumValue;
        public Image bg, fill;
        public void SetValue(float newValue, float minimumValue = -1, float maximumValue = -1)
        {
            if (minimumValue >= 0) { this.minimumValue = minimumValue; }
            if (maximumValue >= 0) { this.maximumValue = maximumValue; }
            fill.fillAmount = (newValue - this.minimumValue) / (this.maximumValue - this.minimumValue);
        }
    }
}
