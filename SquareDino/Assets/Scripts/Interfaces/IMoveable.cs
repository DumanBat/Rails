using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SquareDino.Behaviour
{
    public interface IMoveable
    {
        public Vector3 PreviousTargetPosition { get; set; }
        public void Move(Vector3 pos);
        public void Stop();

        public NavMeshAgent GetNavMeshAgent();
    }
}
