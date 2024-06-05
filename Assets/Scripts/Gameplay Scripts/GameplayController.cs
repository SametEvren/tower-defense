﻿using System;
using Player_Scripts;
using UnityEngine;
using Utility;
using Wave_Scripts;

namespace Gameplay_Scripts
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameProgression gameProgression;
        private int _currentWave;
        private int _health;
        private WaveController WaveController => ServiceLocator.Get<WaveController>();
        private PlayerStatController PlayerStatController => ServiceLocator.Get<PlayerStatController>();

        [SerializeField] private DayNightController dayNightController;

        public GameState gameState;
        public event Action OnGameStarted;
        
        public event Action<int> OnHealthChanged;
        public event Action OnGameOver;

        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        private void Start()
        {
            WaveController.OnWaveFinished += HandleWaveFinished;
        }

        public void StartTheGame()
        {
            gameState = GameState.Running;
            _health = PlayerStatController.StartingHealth;
            OnGameStarted?.Invoke();
        }
        
        private void HandleWaveFinished()
        {
            dayNightController.MakeItDay();
        }

        public void SpawnNextWave()
        {
            dayNightController.MakeItNight();
            var waveData = gameProgression.GetWave(_currentWave);
            WaveController.InitiateWave(waveData);
            _currentWave++;
        }

        public void DecreaseHealth(int amount)
        {
            _health -= amount;
            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
                OnGameOver?.Invoke();
        }
        
        public enum GameState
        {
            StartMenu,
            Running,
            Paused
        }
    }
}