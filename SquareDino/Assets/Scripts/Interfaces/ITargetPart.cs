using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface ITargetPart
    {
        public ITarget OriginTarget { get; }
        public void SetHitData(Vector3 hitPos);
    }
}
