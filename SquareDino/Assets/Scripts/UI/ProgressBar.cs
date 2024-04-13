using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SquareDino.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        public void SetValue(float val) => _slider.value = val;
    }
}
