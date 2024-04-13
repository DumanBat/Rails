using SquareDino.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class HealthBehaviourView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar _healthBar;
        [SerializeField]
        private float _heightOffset;

        private bool _isActive;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            Vector2 screenPos = _camera.WorldToScreenPoint(transform.position);
            _healthBar.transform.position = screenPos + new Vector2(0, _heightOffset * Screen.height);
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            _healthBar.gameObject.SetActive(isActive);
        }

        public void SetHealthBarValue(float val) => _healthBar.SetValue(val);
    }
}
