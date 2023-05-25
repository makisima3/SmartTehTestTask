using Code.Enemies;
using Code.Pooling;
using UnityEngine;
using Zenject;

namespace Code.Player.Shooting.Bullets.Impls
{
    public class TripleBullet : Bullet
    {
        [SerializeField] private float speed = 5f;

        private ObjectPool<Bullet> _pool;
        private Camera _mainCamera;
        private float _screenWidth;
        private float _screenHeight;

        public override void Init(Transform spawnPoint, Vector3 rotation, ObjectPool<Bullet> pool)
        {
            _pool = pool;
            _mainCamera = Camera.main;
            _screenHeight = 2f * _mainCamera.orthographicSize;
            _screenWidth = _screenHeight * _mainCamera.aspect;

            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(rotation);

            for (int i = 0; i < 2; i++)
            {
                var degrees = 45f;
                if (i % 2 == 0)
                    degrees *= -1;

                var angle = Quaternion.Euler(0f, 0f, degrees);

                var bullet = _pool.GetObject() as TripleBullet;
                bullet.InitWithoutSpawn(spawnPoint, rotation + angle.eulerAngles, _pool);
            }
        }

        public void InitWithoutSpawn(Transform spawnPoint, Vector3 rotation, ObjectPool<Bullet> pool)
        {
            _pool = pool;
            _mainCamera = Camera.main;
            _screenHeight = 2f * _mainCamera.orthographicSize;
            _screenWidth = _screenHeight * _mainCamera.aspect;

            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(rotation);
        }

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            Vector3 movement = transform.up * speed * Time.deltaTime;
            transform.position += movement;

            if (!IsWithinScreenBounds(transform.position))
            {
                _pool.ReturnObject(this);
            }
        }

        private bool IsWithinScreenBounds(Vector3 position)
        {
            if (position.x < -_screenWidth / 2f || position.x > _screenWidth / 2f ||
                position.y < -_screenHeight / 2f || position.y > _screenHeight / 2f)
            {
                return false;
            }
            return true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Damage(damage);
            }

            _pool.ReturnObject(this);
        }
    }
}