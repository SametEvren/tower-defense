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

        private void Start()
        {
            WaveController.OnWaveFinished += HandleWaveFinished;
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
    }
}