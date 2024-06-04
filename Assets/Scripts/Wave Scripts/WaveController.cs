using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Wave_Scripts
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private List<WavePoint> wavePoints;
        private readonly Dictionary<int, WavePoint> _wavePointDictionary = new();
        public const float SpawnInterval = 3f;
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
                var wavePoint = _wavePointDictionary[wavePointData.wavePointId];
                wavePoint.ActivateWave(wavePointData.enemySpawnData);
            }
        }
    }
}