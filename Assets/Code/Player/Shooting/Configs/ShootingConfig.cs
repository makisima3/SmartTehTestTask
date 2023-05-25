using System;
using System.Collections.Generic;
using System.Linq;
using Code.Player.Shooting.Bullets;
using Code.Player.Shooting.Enums;
using UnityEngine;

namespace Code.Player.Configs
{
    [Serializable]
    public class TypeToPrefab
    {
        public BulletType Type;
        public Bullet Bullet;
    }
    
    [CreateAssetMenu(fileName = "ShootingConfig", menuName = "ScriptableObjects/Player/ShootingConfig", order = 0)]
    public class ShootingConfig : ScriptableObject
    {

        [SerializeField] private float shootRate = 1;
        [SerializeField] private List<TypeToPrefab> _typeToPrefabs;

        public float ShootRate => shootRate;

        public Bullet GetBullet(BulletType type)
        {
            return _typeToPrefabs.First(ttb => ttb.Type == type).Bullet;
        }
    }
}