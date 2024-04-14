using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField]
        private Transform _aimTarget;
        [SerializeField]
        private List<HumanoidEnemy> _enemies = new List<HumanoidEnemy>();

        public Transform AimTarget => _aimTarget;

        public Action OnEnemyGroupCleared;

        private int _aliveEnemyCount;

        private void Awake()
        {
            _aliveEnemyCount = _enemies.Count;

            foreach (var enemy in _enemies)
                enemy.OnKilled += CheckForGroupClear;
        }

        public void EnableEnemies()
        {
            foreach (var enemy in _enemies)
                enemy.SetActive(true);
        }

        private void CheckForGroupClear()
        {
            _aliveEnemyCount -= 1;

            if (_aliveEnemyCount == 0)
                OnEnemyGroupCleared?.Invoke();
        }
    }
}
