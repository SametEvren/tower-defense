using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;

namespace Wave_Scripts
{
    public class WaveController : MonoBehaviour
    {
        public Wave wave;
        public int currentWaveIndex;
        public List<WaveDatum> waveData;

        private void Start()
        {
            waveData = new List<WaveDatum>(wave.waveData);
        }
    }
}
