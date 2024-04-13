using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface ITarget
    {
        public IHealth Health { get; }
    }
}
