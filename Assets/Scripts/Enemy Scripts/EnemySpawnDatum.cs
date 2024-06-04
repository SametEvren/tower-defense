using System;

namespace Enemy_Scripts
{
    [Serializable]
    public class EnemySpawnDatum
    {
        public EnemyType enemyType;
        public int spawnCount;
        public int wavePointId;
    }
}