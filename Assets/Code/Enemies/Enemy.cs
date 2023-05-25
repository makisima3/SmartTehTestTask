using Code.Player.BulletCollectables;
using Code.Player.Configs;
using DG.Tweening;
using Plugins.MyUtils;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Enemies
{
   
    public class Enemy : MonoBehaviour
    {
        [Inject] private EnemiesConfig enemiesConfig;
        [Inject] private DiContainer container;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed;
        [SerializeField] private int hp;
        [SerializeField] private float wakeupTime = 1f;

        private Camera _mainCamera;
        private float _screenWidth;
        private float _screenHeight;
        private bool _isWakeup;

        public bool IsAlive => hp > 0;
        public UnityEvent<Enemy> OnDead { get; private set; }
        
        public void Init()
        {
            OnDead = new UnityEvent<Enemy>();
            
            _mainCamera = Camera.main;
            _screenHeight = 2f * _mainCamera.orthographicSize;
            _screenWidth = _screenHeight * _mainCamera.aspect;

            RandomRotate();
            SetRandomPosition();
                
            //Хардкод так как предпологается что правильно сделланные ассеты спрайтов кораблей будут иметь свой цвет а в юнити он будет всегда белый
            spriteRenderer.color = new Color(255f, 255f, 255f, 0f);
            spriteRenderer.DOColor(Color.white, wakeupTime).OnComplete(() => _isWakeup = true);
        }

        public void Damage(int damage)
        {
            if(!IsAlive || !_isWakeup)
                return;
            
            hp -= damage;
            
            spriteRenderer.DOColor(enemiesConfig.DamageColor, enemiesConfig.AnimTime).OnComplete(() =>
            {
                spriteRenderer.DOColor(Color.white, enemiesConfig.AnimTime);
            });

            if (!IsAlive)
                Death();
        }
        
        private void SetRandomPosition()
        {
            float randomX = Random.Range(-_screenWidth / 2f, _screenWidth / 2f);
            float randomY = Random.Range(-_screenHeight / 2f, _screenHeight / 2f);
            transform.position = new Vector3(randomX, randomY, 0f);
        }

        private void Death()
        {
            if(IsDrop())
                container.InstantiatePrefabForComponent<BulletCollectable>
                    (enemiesConfig.BulletCollectablesPrefabs.ChooseOne(),transform.position,quaternion.identity,null);
            
            OnDead.Invoke(this);
            Destroy(gameObject);
        }

        private bool IsDrop()
        {
            return Random.Range(0f, 1f) >= enemiesConfig.DropChance;
        }

        private void RandomRotate()
        {
            float randomAngle = Random.Range(0f, 360f);  // Генерируем случайный угол поворота от 0 до 360 градусов
            transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);  // Устанавливаем поворот объекта
        }
        
        private void Update()
        {
            if(!_isWakeup)
                return;
            
            MoveForward();
            
            if (!IsWithinScreenBounds(transform.position))
            {
                WrapAroundScreen();
            }
        }

        private void MoveForward()
        {
            Vector3 movement = transform.up * speed * Time.deltaTime;
            transform.position += movement;
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

        private void WrapAroundScreen()
        {
            Vector3 currentPosition = transform.position;
            
            if (currentPosition.x < -_screenWidth / 2f)
            {
                currentPosition.x = _screenWidth / 2f;
            }
            else if (currentPosition.x > _screenWidth / 2f)
            {
                currentPosition.x = -_screenWidth / 2f;
            }

            if (currentPosition.y < -_screenHeight / 2f)
            {
                currentPosition.y = _screenHeight / 2f;
            }
            else if (currentPosition.y > _screenHeight / 2f)
            {
                currentPosition.y = -_screenHeight / 2f;
            }

            transform.position = currentPosition;
        }
        
    }
}