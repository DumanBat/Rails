using SquareDino.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class GameplayMediator : MonoBehaviour
    {
        [SerializeField]
        private GameConfig _config;
        [SerializeField]
        private PlayerController _playerPrefab;

        private PlayerController _player;
        private Level _currentLevel;

        public void Start()
        {
            _currentLevel = Instantiate(GetRandomLevel());
            _player = Instantiate(_playerPrefab);
            _player.Init(_config, _currentLevel.Nodes);
        }

        private Level GetRandomLevel()
        {
            var levelIndex = Random.Range(0, _config.Levels.Count);
            return _config.Levels[levelIndex];
        }
    }
}
