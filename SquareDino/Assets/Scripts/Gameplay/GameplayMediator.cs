using SquareDino.Player;
using SquareDino.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SquareDino.Gameplay
{
    public class GameplayMediator : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private GameConfig _config;
        [SerializeField]
        private PlayerController _playerPrefab;

        [Space]
        [Header("References")]
        [SerializeField]
        private CameraFollow _cameraFollow;
        [SerializeField]
        private PlayerUI _playerUI;

        private PlayerController _player;
        private Level _currentLevel;

        public void Start()
        {
            InitLevel();
        }

        private void InitLevel()
        {
            _currentLevel = Instantiate(GetRandomLevel());
            _player = Instantiate(_playerPrefab);
            _player.Init(_config, _cameraFollow, _currentLevel.Nodes);
            _player.OnGameEnded += StopGame;

            _playerUI.SetActiveStartScreen(true);
            _playerUI.StartPressed += StartGame;
            _playerUI.RestartPressed += RestartGame;
        }

        private Level GetRandomLevel()
        {
            var levelIndex = Random.Range(0, _config.Levels.Count);
            return _config.Levels[levelIndex];
        }

        private void StartGame()
        {
            _player.Enable();
            _playerUI.SetActiveStartScreen(false);
        }

        private void StopGame()
        {
            _playerUI.SetActiveEndScreen(true);
        }

        private void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
