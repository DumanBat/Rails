using SquareDino.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private GameConfig _config;

        private float _moveSpeed;
        private IMoveable _moveable;

        private void Awake()
        {
            _moveable = GetComponent<IMoveable>();
            Init(_config);
        }

        public void Init(GameConfig config)
        {
            _config = config;
            _moveSpeed = config.MoveSpeed;

            _moveable.GetNavMeshAgent().speed = _moveSpeed;
        }
    }
}
