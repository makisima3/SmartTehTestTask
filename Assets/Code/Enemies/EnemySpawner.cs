using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Enemies.Enums;
using Code.Player.Configs;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Code.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private EnemiesConfig enemiesConfig;
        [Inject] private DiContainer container;

        [SerializeField] private Transform enemiesHolder;
        
        private int _waveIndex = -1;
        private List<Enemy> _enemies;

        public UnityEvent OnEnemyDeadEvent { get; private set; }
        public UnityEvent<int,int> OnWaveStart { get; private set; }
        
        private void Awake()
        {
            _enemies = new List<Enemy>();
            OnEnemyDeadEvent = new UnityEvent();
            OnWaveStart = new UnityEvent<int,int>();
        }

        private void Start()
        {
            StartEnemyWave();
        }

        public void StartEnemyWave()
        {
            _waveIndex = enemiesConfig.GetWave(_waveIndex + 1, out var wave);

            OnWaveStart.Invoke(_waveIndex,enemiesConfig.WavesCount);
            StartCoroutine(SpawnCoroutine(wave));
        }

        private void SpawnEnemy(EnemyType type)
        {
            var enemy = container.InstantiatePrefabForComponent<Enemy>(enemiesConfig.GetEnemyPrefab(type), enemiesHolder);
            enemy.Init();
            enemy.OnDead.AddListener(OnEnemyDead);
            
            _enemies.Add(enemy);
        }

        private void OnEnemyDead(Enemy enemy)
        {
            _enemies.Remove(enemy);
            OnEnemyDeadEvent.Invoke();
            
            if(!_enemies.Any())
                StartEnemyWave();
        }

        private IEnumerator SpawnCoroutine(Wave wave)
        {
            foreach (var data in wave.Data)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    SpawnEnemy(data.Type);

                    yield return new WaitForSeconds(1f / enemiesConfig.SpawnRate);
                }
            }
        }
    }
}