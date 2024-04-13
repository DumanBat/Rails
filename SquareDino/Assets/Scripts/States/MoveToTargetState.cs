using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class MoveToTargetState : IState
    {
        private static readonly int WalkHash = Animator.StringToHash("isWalking");
        private const float MinimumRequiredDistance = 0.5f;
        public StateMachine.States State => StateMachine.States.MoveToTarget;

        private PlayerController _player;
        private Animator _animator;
        private IMoveable _moveable;
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;

        public MoveToTargetState(PlayerController player, IMoveable moveable)
        {
            _player = player;
            _animator = player.Animator;
            _moveable = moveable;
            _navMeshAgent = moveable.GetNavMeshAgent();
        }

        public void Tick()
        {
            var targetPosition = _player.TargetWaypoint.GetPosition();
            var distanceToTarget = Vector3.Distance(_player.transform.position, targetPosition);

            if (distanceToTarget < MinimumRequiredDistance)
            {
                _player.TargetWaypoint.WaypointReached?.Invoke();
                _player.IsActive = !_player.TargetWaypoint.IsEnd;
                _player.IsWaypointReached = true;
            }
            else
                _moveable.Move(targetPosition);
        }
        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _animator.SetBool(WalkHash, true);
        }

        public void OnExit()
        {
            _moveable.Stop();
            _navMeshAgent.enabled = false;
            _animator.SetBool(WalkHash, false);
        }

        public void FixedTick() { }
    }
}
