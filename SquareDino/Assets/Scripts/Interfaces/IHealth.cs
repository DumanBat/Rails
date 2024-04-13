using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public interface IHealth
    {
        public delegate void HealthChange(float val);
        public delegate void Death();

        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public HealthChange OnDamaged { get; set; }
        public Death OnDeath { get; set; }
        public void TakeDamage(float damage);
    }
}
