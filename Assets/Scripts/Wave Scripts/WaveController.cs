using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
using Utility;

namespace Wave_Scripts
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private List<WavePoint> wavePoints;
        public List<Enemy> enemiesInGame;
        
        private readonly Dictionary<int, WavePoint> _wavePointDictionary = new();
        public const float SpawnInterval = 3f;

        public event Action OnWaveFinished;

        private int _enemyCount;

        private void Awake()
        {
            ServiceLocator.Add(this);

            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            foreach (var wavePoint in wavePoints)
            {
                if(!_wavePointDictionary.TryAdd(wavePoint.Id, wavePoint))
                    Debug.LogError($"Can't add wave point to dictionary. The key is: {wavePoint.Id}");
            }
        }
        
        public void InitiateWave(WaveData waveData)
        {
            foreach (var wavePointData in waveData.wavePointData)
            {
                foreach (var enemySpawnData in wavePointData.enemySpawnData)
                {
                    _enemyCount += enemySpawnData.spawnCount;
                }
                
                var wavePoint = _wavePointDictionary[wavePointData.wavePointId];
                
                wavePoint.ActivateWave(wavePointData.enemySpawnData);
            }
        }

        public void HandleEnemySpawned(Enemy enemy)
        {
            enemiesInGame.Add(enemy);
            enemy.OnEnemyDeath += HandleEnemyDeath;
        }

        public void HandleEnemyDeath(Enemy enemy)
        {
            enemy.OnEnemyDeath -= HandleEnemyDeath;
            enemiesInGame.Remove(enemy);
            _enemyCount--;
            
            if (enemiesInGame.Count <= 0 && _enemyCount <= 0)
            {
                OnWaveFinished?.Invoke();
            }
        }
    }
}