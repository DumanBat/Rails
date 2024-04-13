using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class WaypointNode : MonoBehaviour
    {
        [SerializeField]
        private bool _isStart;
        [SerializeField]
        private bool _isEnd;
        [SerializeField]
        private bool _isShootingPoint;
        [SerializeField]
        private EnemyGroup _enemyGroup;

        public bool IsStart => _isStart;
        public bool IsEnd => _isEnd;
        public bool IsShootingPoint => _isShootingPoint;

        public Action WaypointReached;
        public Action WaypointCleared;

        private void Awake()
        {
            if (_isShootingPoint)
            {
                WaypointReached += EnableEnemyGroup;
                _enemyGroup.OnEnemyGroupCleared += () => WaypointCleared?.Invoke();
            }
        }

        public Vector3 GetPosition() => transform.position;

        public Transform GetAimTarget() => _enemyGroup.AimTarget;

        private void EnableEnemyGroup()
        {
            _enemyGroup.EnableEnemies();
        }
    }
}
