using SquareDino.Behaviour;
using SquareDino.Gameplay;
using SquareDino.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private GameConfig _config;
        private StateMachine _stateMachine;
        private float _moveSpeed;
        private IMoveable _moveable;

        private IState _idle;

        private List<WaypointNode> _path;

        public Animator Animator => _animator;
        public bool IsGameStarted { get; set; }
        public bool IsActive { get; set; }
        public bool IsWaypointReached { get; set; }
        public bool IsNoMoreWaypoints { get; set; }
        public bool IsAttackEnded { get; set; }

        public bool IsTargetShootingPoint => TargetWaypoint.IsShootingPoint;
        public bool HasNextTarget => TargetWaypoint != null;
        public WaypointNode TargetWaypoint { get; set; }

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _moveable = GetComponent<IMoveable>();
        }

        private void Update()
        {
            _stateMachine.Tick();
            if (IsActive)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsActive = true;
                IsGameStarted = true;
            }
        }

        public void Init(GameConfig config, List<WaypointNode> path)
        {
            _config = config;
            _path = path;
            _moveSpeed = config.MoveSpeed;

            _moveable.GetNavMeshAgent().speed = _moveSpeed;
            InitStates();
        }

        private void InitStates()
        {
            _idle = new IdleState(Animator);
            var searchForTarget = new SearchForTargetState(this, _path);
            var moveToTarget = new MoveToTargetState(this, _moveable);
            var attack = new AttackState(this, Animator);

            At(_idle, searchForTarget, () => IsGameStarted && IsActive);
            At(searchForTarget, moveToTarget, () => HasNextTarget);
            At(moveToTarget, attack, () => IsWaypointReached && IsTargetShootingPoint);
            At(moveToTarget, searchForTarget, () => IsWaypointReached && !IsTargetShootingPoint && !IsNoMoreWaypoints);
            At(attack, searchForTarget, () => IsAttackEnded && !IsNoMoreWaypoints);
            At(moveToTarget, _idle, () => IsWaypointReached && IsNoMoreWaypoints);
            At(attack, _idle, () => IsAttackEnded && IsNoMoreWaypoints);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(_idle);
        }
    }
}
