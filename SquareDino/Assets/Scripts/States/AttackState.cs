using SquareDino.Gameplay;
using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class AttackState : IState
    {
        private static readonly int AttackHash = Animator.StringToHash("isAttacking");
        public StateMachine.States State => StateMachine.States.Attack;

        private IShootable _shootable;
        private CameraFollow _cameraFollow;
        private Camera _mainCamera;
        private PlayerController _player;
        private Animator _animator;
        private Weapon _weapon;
        private WaypointNode _currentWaypoint;

        public AttackState(PlayerController player, Animator animator, CameraFollow cameraFollow, IShootable shootable)
        {
            _shootable = shootable;
            _cameraFollow = cameraFollow;
            _mainCamera = Camera.main;
            _player = player;
            _animator = animator;
        }

        public void OnEnter()
        {
            _player.IsAttackEnded = false;
            _currentWaypoint = _player.TargetWaypoint;
            _currentWaypoint.WaypointCleared += FinishShootingSection;

            _cameraFollow.SetActiveAimMode(true);
            LookAtCurrentTarget();

            _animator.SetBool(AttackHash, true);
        }

        public void OnExit()
        {
            _cameraFollow.SetActiveAimMode(false);
            _animator.SetBool(AttackHash, false);
            _currentWaypoint.WaypointCleared -= FinishShootingSection;
        }

        public void Tick()
        {
            LookAtCurrentTarget();

            if (Input.GetMouseButtonDown(0))
                _shootable.Shoot();
        }

        private void FinishShootingSection()
        {
            _player.IsAttackEnded = true;
        }

        private void LookAtCurrentTarget()
        {
            var aimTarget = _currentWaypoint.GetAimTarget();
            _player.transform.LookAt(aimTarget);
        }

        public void FixedTick() { }
    }
}
