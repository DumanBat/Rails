using SquareDino.Behaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    public class HumanoidEnemy : MonoBehaviour, ITarget
    {
        private static readonly int Damaged = Animator.StringToHash("Damaged");

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private HealthBehaviourView _healthView;

        private IHealth _health;
        private IRagdoll _ragdoll;
        private Vector3 _lastHitPos;

        public Action OnKilled;

        public IHealth Health => _health;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _ragdoll = GetComponent<IRagdoll>();

            _healthView.SetActive(false);
            _ragdoll.TurnOff();

            _health.OnDeath += Kill;
            _health.OnDamaged += (damage) => TakeDamage();
            _health.OnDamaged += DisplayHealthData;
            DisplayHealthData(1f);
        }

        public void SetActive(bool isActive)
        {
            _healthView.SetActive(isActive);
        }

        private void TakeDamage()
        {
            _animator.SetTrigger(Damaged);
        }

        private void DisplayHealthData(float health)
        {
            _healthView.SetHealthBarValue(health);
        }

        private void Kill()
        {
            SetActive(false);
            _animator.enabled = false;
            _ragdoll.TurnOn();
            _ragdoll.ApplyForce(_lastHitPos);
            OnKilled?.Invoke();
        }
    }
}