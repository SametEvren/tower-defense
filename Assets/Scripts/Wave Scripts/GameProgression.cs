using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;

namespace Wave_Scripts
{
    [CreateAssetMenu(fileName = "Game Progression", menuName = "Game/Game Progression")]
    public class GameProgression : ScriptableObject
    {
        public List<WaveData> waves;

        public WaveData GetWave(int currentWave)
        {
            return waves[currentWave];
        }
    }
    
    [Serializable]
    public class WaveData
    {
        public List<WavePointData> wavePointData;
    }
    
    [Serializable]
    public class WavePointData
    {
        public int wavePointId;
        public List<EnemySpawnData> enemySpawnData;
    }

    [Serializable]
    public class EnemySpawnData
    {
        public EnemyType enemyType;
        public int spawnCount;
    }
}