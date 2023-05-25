using System;
using System.Collections.Generic;
using System.Linq;
using Code.Enemies;
using Code.Enemies.Enums;
using Code.Player.BulletCollectables;
using UnityEngine;

namespace Code.Player.Configs
{
    [Serializable]
    public class TypeToEnemy
    {
        public EnemyType Type;
        public Enemy EnemyPrefab;
    }
    
    [Serializable]
    public class WaveData
    {
        public EnemyType Type;
        public int Count;
    }
    
    [Serializable]
    public class Wave
    {
        public List<WaveData> Data;
    }
    
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/Enemies/EnemiesConfig", order = 0)]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private float spawnRate = 0.5f;
        [SerializeField] private List<TypeToEnemy> typeToPrefabs;
        [SerializeField] private List<Wave> waves;
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private float animTime = 0.2f;
        [SerializeField] private List<BulletCollectable> bulletCollectablesPrefabs;
        [SerializeField,Range(0f,1f)] private float dropChance = 0.5f;

        public List<BulletCollectable> BulletCollectablesPrefabs => bulletCollectablesPrefabs;

        public float DropChance => dropChance;

        public int WavesCount => waves.Count;
        public float SpawnRate => spawnRate;

        public Color DamageColor => damageColor;

        public float AnimTime => animTime;

        public Enemy GetEnemyPrefab(EnemyType type)
        {
            return typeToPrefabs.First(ttp => ttp.Type == type).EnemyPrefab;
        }

        public int GetWave(int index, out Wave wave)
        {
            if (index >= waves.Count)
                index = 0;

            wave = waves[index];

            return index;
        }
    }
}