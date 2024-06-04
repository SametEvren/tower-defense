using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Enemy_Scripts;
using Spawn;
using UnityEngine;
using Utility;

namespace Wave_Scripts
{
    public class WavePoint : MonoBehaviour
    {
        [SerializeField] private int wavePointId;
        public int Id => wavePointId;
        
        public SplineComputer splineComputer;
        
        private SpawnController SpawnController => ServiceLocator.Get<SpawnController>();
     
        public void ActivateWave(List<EnemySpawnData> enemySpawnData)
        {
            StartCoroutine(SpawnEnemyWave(enemySpawnData));
        }

        private IEnumerator SpawnEnemyWave(List<EnemySpawnData> enemySpawnData)
        {
            foreach (var enemyData in enemySpawnData)
            {
                for (int i = 0; i < enemyData.spawnCount; i++)
                {
                    SpawnEnemy(enemyData.enemyType);
                    yield return new WaitForSeconds(WaveController.SpawnInterval);
                }
            }
        }

        private void SpawnEnemy(EnemyType enemyType)
        {
            var enemy = SpawnController.GetItemFromPool(enemyType);
            var enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.SetMoveFeature(this);
        }
    }
}