using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface IRagdoll
    {
        public void TurnOn();
        public void TurnOff();
        public void ApplyForce(Vector3 direction);
    }
}
