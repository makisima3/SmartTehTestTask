using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player.Configs;
using Code.Player.Shooting.Bullets;
using Code.Player.Shooting.Enums;
using Code.Player.Shooting.Pools;
using Code.Pooling;
using UnityEngine;
using Zenject;

namespace Code.Player.Shooting
{
    public class ShootController : MonoBehaviour
    {
        [Inject] private ShootingConfig shootingConfig;
        [Inject] private DiContainer container;

        [SerializeField] private Transform bulletsHoldersHolder;
        [SerializeField] private Transform spawnPoint;
        
        private Coroutine _shootCoroutine;
        private Dictionary<BulletType, ObjectPool<Bullet>> _typeToPool;
        private BulletType _currentType;

        private void Awake()
        {
            _typeToPool = new Dictionary<BulletType, ObjectPool<Bullet>>();

            foreach (BulletType type in Enum.GetValues(typeof(BulletType)))
            {
                var holder = new GameObject();
                holder.transform.SetParent(bulletsHoldersHolder);
                holder.name = $"{type} bulletsHolder";
                
                _typeToPool.Add(type, new BaseBulletsPool(shootingConfig.GetBullet(type),10,container,holder.transform,type));
            }
            
            StartShoot();
        }

        public void SetBulletType(BulletType type)
        {
            _currentType = type;
        }
        
        public void StartShoot()
        {
            StopShoot();

            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }


        public void StopShoot()
        {
            if (_shootCoroutine == null)
                return;
            
            StopCoroutine(_shootCoroutine);
            _shootCoroutine = null;
        }

        private void Shoot()
        {
            var pool = _typeToPool[_currentType];
            var bullet = pool.GetObject();
            bullet.Init(spawnPoint,transform.rotation.eulerAngles,pool);
        }
        
        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                Shoot();

                yield return new WaitForSeconds(1f / shootingConfig.ShootRate);
            }
        }
    }
}