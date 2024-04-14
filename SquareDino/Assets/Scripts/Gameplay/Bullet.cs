using SquareDino.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SquareDino.Gameplay
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rb;
        private ObjectPool<Bullet> _originPool;
        private float _damage;
        private float _bulletSpeed;
        private Vector3 _direction;
        private bool _isActive;

        public Rigidbody Rigidbody => _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            transform.Translate(_direction * _bulletSpeed * Time.deltaTime);
        }

        public void Init(ObjectPool<Bullet> originPool, float damage, float speed)
        {
            _originPool = originPool;
            _damage = damage;
            _bulletSpeed = speed;
        }

        public void Launch(Vector3 direction)
        {
            _direction = direction;
            _isActive = true;
        }

        public void Unload()
        {
            _direction = Vector3.zero;
            _isActive = false;
            _originPool.Release(this);
        }

        public void OnCollisionEnter(Collision collision)
        {
            Unload();
        }

        public void OnTriggerEnter(Collider other)
        {
            var targetPart = other.GetComponent<ITargetPart>();
            if (targetPart != null)
            {
                targetPart.SetHitData(transform.position);
                targetPart.OriginTarget.Health.TakeDamage(_damage);
            }
            Unload();
        }
    }
}
