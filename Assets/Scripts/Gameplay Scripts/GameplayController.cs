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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnNextWave();
            }
        }

        private void SpawnNextWave()
        {
            var waveData = gameProgression.GetWave(currentWave);
            WaveController.InitiateWave(waveData);
            currentWave++;
        }
    }
}