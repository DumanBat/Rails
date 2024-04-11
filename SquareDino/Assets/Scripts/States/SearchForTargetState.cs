using SquareDino.Gameplay;
using SquareDino.Player;
using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class SearchForTargetState : IState
    {
        public StateMachine.States State => StateMachine.States.SearchForTarget;

        private PlayerController _player;
        private List<WaypointNode> _path;
        private int _currentTargetWaypointIndex;

        public SearchForTargetState(PlayerController player, List<WaypointNode> path)
        {
            _player = player;
            _path = path;
            _currentTargetWaypointIndex = 0;
        }

        public void OnEnter()
        {
            _currentTargetWaypointIndex += 1;
            var targetWaypoint = _path[_currentTargetWaypointIndex];

            _player.IsWaypointReached = false;
            _player.IsNoMoreWaypoints = targetWaypoint.IsEnd;
            _player.TargetWaypoint = targetWaypoint;
        }

        public void FixedTick() { }

        public void OnExit() { }

        public void Tick() { }
    }
}
