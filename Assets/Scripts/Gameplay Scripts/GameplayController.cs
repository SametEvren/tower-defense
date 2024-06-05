using System;
using UnityEngine;
using Utility;
using Wave_Scripts;

namespace Gameplay_Scripts
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameProgression gameProgression;
        private int currentWave;
        
        private WaveController WaveController => ServiceLocator.Get<WaveController>();

        [SerializeField] private DayNightController dayNightController;

        public GameState gameState;
        public event Action OnGameStarted;

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
            OnGameStarted?.Invoke();
        }
        
        private void HandleWaveFinished()
        {
            dayNightController.MakeItDay();
        }

        public void SpawnNextWave()
        {
            dayNightController.MakeItNight();
            var waveData = gameProgression.GetWave(currentWave);
            WaveController.InitiateWave(waveData);
            currentWave++;
        }
        
        public enum GameState
        {
            StartMenu,
            Running,
            Paused
        }
    }
}