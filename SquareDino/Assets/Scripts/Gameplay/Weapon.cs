using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SquareDino.Gameplay
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Transform _firepoint;
        [SerializeField]
        private Bullet _bulletPrefab;
        [SerializeField]
        private AudioSource _shootingSound;
        [SerializeField]
        private int _bulletPoolSize;

        [SerializeField]
        private float _damage;
        [SerializeField]
        private float _bulletSpeed;

        private ObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            InitBulletPool();
        }

        private void InitBulletPool()
        {
            _bulletPool = new ObjectPool<Bullet>(() =>
            {
                var bullet = Instantiate(_bulletPrefab);
                bullet.Init(_bulletPool, _damage, _bulletSpeed);
                return bullet;
            },
            bullet =>
            {
                bullet.gameObject.SetActive(true);
                bullet.transform.position = _firepoint.position;
                bullet.Rigidbody.velocity = Vector3.zero;
            },
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(bullet.gameObject),
            false, _bulletPoolSize, _bulletPoolSize);
        }

        public void Fire(Vector3 position)
        {
            _shootingSound.Play();

            var direction = (position - _firepoint.position).normalized;
            _bulletPool.Get().Launch(direction);
        }
    }
}
