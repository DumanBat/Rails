using SquareDino.Behaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SquareDino.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour, ITarget
    {
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Damaged = Animator.StringToHash("Damaged");

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private HealthBehaviourView _healthView;

        private IHealth _health;
        private Collider _collider;

        public Action OnKilled;

        public IHealth Health => _health;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _health = GetComponent<IHealth>();
            _healthView.SetActive(false);

            _health.OnDeath += Kill;
            _health.OnDamaged += (damage) => TakeDamage();
            _health.OnDamaged += DisplayHealthData;
            DisplayHealthData(1f);
        }

        public void SetActive(bool isActive)
        {
            _collider.enabled = isActive;
            _healthView.SetActive(true);
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
            _animator.SetTrigger(Dead);
            _healthView.SetActive(false);
            OnKilled?.Invoke();
        }
    }
}