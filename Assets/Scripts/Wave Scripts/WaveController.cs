using System.Collections.Generic;
using Spawn;
using UnityEngine;
using Utility;

namespace Wave_Scripts
{
    public class WaveController : MonoBehaviour
    {
        public Wave wave;
        public int currentWaveIndex;
        public int currentSpawnIndex;
        public List<WavePoint> wavePoints;
        public List<WaveDatum> waveData;

        private float _spawnTimer = 0f;
        private float _spawnInterval = 3f;

        private SpawnController _spawnController;
        
        private void Start()
        {
            _spawnController = ServiceLocator.Get<SpawnController>();
            waveData = new List<WaveDatum>(wave.waveData);
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (waveData[currentWaveIndex].enemySpawnData.Count <= currentSpawnIndex)
            {
                IterateWave();
                return;
            }
            
            if (_spawnTimer >= _spawnInterval)
            {
                SpawnEnemy();
            }
        }

        private void IterateWave()
        {
            currentSpawnIndex = 0;
            currentWaveIndex++;
        }

        private void SpawnEnemy()
        {
            _spawnTimer = 0;
            
            var spawnDatum = waveData[currentWaveIndex].enemySpawnData[currentSpawnIndex];
            var enemyType = spawnDatum.enemyType;
            var spawnCount = spawnDatum.spawnCount;
            var wavePointId = spawnDatum.wavePointId;

            _spawnController.GenerateSpawn(enemyType, spawnCount, wavePointId);
            currentSpawnIndex++;
        }
    }
}
