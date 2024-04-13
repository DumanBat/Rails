using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Behaviour
{
    public class HealthBehaviour : MonoBehaviour, IHealth
    {
        [SerializeField]
        private float _maxHealth;

        private float _health;
        public float Health
        {
            get => _health;
            set
            {
                if (value >= MaxHealth)
                    _health = MaxHealth;
                else
                    _health = value <= 0 ? 0 : value;
            }
        }
        public float MaxHealth { get; set; }
        public IHealth.Death OnDeath { get; set; }
        public IHealth.HealthChange OnDamaged { get; set; }

        private void Awake()
        {
            MaxHealth = _maxHealth;
            Health = _maxHealth;
        }

        public void TakeDamage(float val)
        {
            if (_health <= 0)
                return;

            Health -= val;
            OnDamaged?.Invoke(Health / MaxHealth);
            if (_health <= 0)
                OnDeath?.Invoke();
        }
    }
}
