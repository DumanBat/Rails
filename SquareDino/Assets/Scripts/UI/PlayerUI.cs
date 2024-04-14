using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace SquareDino.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Start Panel")]
        [SerializeField]
        private Transform _startPanel;
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private TextMeshProUGUI _introText;

        [Space]
        [Header("End Panel")]
        [SerializeField]
        private Transform _endPanel;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _exitButton;
        [SerializeField]
        private TextMeshProUGUI _winningText;

        public Action StartPressed;
        public Action RestartPressed;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => StartPressed?.Invoke());
            _restartButton.onClick.AddListener(() => RestartPressed?.Invoke());
            _exitButton.onClick.AddListener(() => Application.Quit());

            SetActiveEndScreen(false);
        }

        public void SetActiveStartScreen(bool isActive)
        {
            _startPanel.gameObject.SetActive(isActive);
        }

        public void SetActiveEndScreen(bool isActive)
        {
            _endPanel.gameObject.SetActive(isActive);
        }
    }
}
