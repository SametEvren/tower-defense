using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;

namespace Wave_Scripts
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Wave/New Wave")]
    public class Wave : ScriptableObject
    {
        public List<WaveDatum> waveData;
    }

    [Serializable]
    public class WaveDatum
    {
        public List<EnemySpawnDatum> enemySpawnData;
    }

    
}