using System;
using Code.Enemies;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class ScoreView : MonoBehaviour
    {
        [Inject] private EnemySpawner enemySpawner;
        
        [SerializeField] private TMP_Text scorePlace;

        //в полноценном проекте вынести это в данные игрока
        private int _score;

        private void Start()
        {
            enemySpawner.OnEnemyDeadEvent.AddListener(AddScore);
            scorePlace.text = $"X{0}";
        }

        public void AddScore()
        {
            _score += 1;

            scorePlace.text = $"X{_score}";
        }
    }
}