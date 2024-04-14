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
        [SerializeField]
        private Transform _weaponHolder;
        [SerializeField]
        private Weapon _weaponPrefab;

        private GameConfig _config;
        private CameraFollow _camera;
        private StateMachine _stateMachine;
        private Weapon _weapon;
        private float _moveSpeed;
        private IMoveable _moveable;
        private IShootable _shootable;

        private IState _idle;

        private List<WaypointNode> _path;

        public Action OnGameEnded;

        public Animator Animator => _animator;
        public bool IsGameStarted { get; set; }
        public bool IsGameEnded { get; set; }
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
            _shootable = GetComponent<IShootable>();
            _weapon = Instantiate(_weaponPrefab, _weaponHolder);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void Init(GameConfig config, CameraFollow camera, List<WaypointNode> path)
        {
            _config = config;
            _camera = camera;
            _path = path;
            _moveSpeed = config.MoveSpeed;

            _moveable.GetNavMeshAgent().speed = _moveSpeed;
            _shootable.Init(_weapon, _camera.MainCamera);
            _camera.Init(transform);
            InitStates();
        }

        public void Enable()
        {
            IsActive = true;
            IsGameStarted = true;
        }

        private void InitStates()
        {
            _idle = new IdleState(this, Animator);
            var searchForTarget = new SearchForTargetState(this, _path);
            var moveToTarget = new MoveToTargetState(this, _moveable);
            var attack = new AttackState(this, _camera, _shootable);
            var finalReached = new FinalReachedState(this);

            At(_idle, searchForTarget, () => IsGameStarted && IsActive);
            At(searchForTarget, moveToTarget, () => HasNextTarget);

            At(moveToTarget, attack, () => IsWaypointReached && IsTargetShootingPoint);
            At(moveToTarget, searchForTarget, () => IsWaypointReached && !IsTargetShootingPoint && !IsNoMoreWaypoints);
            At(moveToTarget, finalReached, () => IsWaypointReached && IsNoMoreWaypoints && !IsTargetShootingPoint);

            At(attack, searchForTarget, () => IsAttackEnded && !IsNoMoreWaypoints);
            At(attack, finalReached, () => IsAttackEnded && IsNoMoreWaypoints);
            At(attack, _idle, () => IsAttackEnded && IsNoMoreWaypoints);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(_idle);
        }
    }
}
