using SquareDino.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface IState
    {
        StateMachine.States State { get; }

        void Tick();
        void FixedTick();
        void OnEnter();
        void OnExit();
    }
}
