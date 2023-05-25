using System;
using Code.Enemies;
using Code.Player.Configs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class WaveView : MonoBehaviour
    {
        [Inject] private EnemySpawner enemySpawner;
        
        [SerializeField] private TMP_Text wavePlace;

        private void Start()
        {
            enemySpawner.OnWaveStart.AddListener(ShowWave);
        }

        private void ShowWave(int waveIndex, int maxWave)
        {
            wavePlace.text = $"Wave {waveIndex + 1}/{maxWave}";
        }
    }
}