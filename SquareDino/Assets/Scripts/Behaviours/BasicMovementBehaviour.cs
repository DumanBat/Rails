using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SquareDino.Behaviour
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicMovementBehaviour : MonoBehaviour, IMoveable
    {
        private NavMeshAgent _navMeshAgent;
        public Vector3 PreviousTargetPosition { get; set; }

        private void Awake() => _navMeshAgent = GetComponent<NavMeshAgent>();

        public void Move(Vector3 pos)
        {
            if (PreviousTargetPosition == pos)
                return;

            PreviousTargetPosition = pos;
            _navMeshAgent.SetDestination(pos);
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            PreviousTargetPosition = Vector3.zero;
        }

        public NavMeshAgent GetNavMeshAgent() => _navMeshAgent;
    }
}
