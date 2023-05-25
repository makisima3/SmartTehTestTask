using System;
using Code.Player.Configs;
using Code.Player.Shooting;
using Code.Player.Shooting.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Player.BulletCollectables
{
    public class BulletCollectable : MonoBehaviour
    {
        [Inject] private BulletCollectablesConfig bulletCollectablesConfig;
        
        [SerializeField] private BulletType type;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer.color = bulletCollectablesConfig.GetColor(type);

            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ShootController>(out var shootController))
            {
                shootController.SetBulletType(type);
                Destroy(gameObject);
            }
        }
    }
}