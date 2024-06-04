using Dreamteck.Splines;
using UnityEngine;
using Wave_Scripts;

namespace Enemy_Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed;
        internal SplineFollower _splineFollower;

        private void Awake()
        {
            _splineFollower = GetComponent<SplineFollower>();
        }

        public void SetMoveFeature(WavePoint wavePoint)
        {
            transform.position = wavePoint.transform.position;
            _splineFollower.spline = wavePoint.splineComputer;
        }
    }
}