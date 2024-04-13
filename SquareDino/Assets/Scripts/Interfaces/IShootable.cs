using SquareDino.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface IShootable
    {
        public void Init(Weapon weapon, Camera camera);
        public void Shoot();
    }
}
