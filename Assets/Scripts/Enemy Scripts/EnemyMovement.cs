using Dreamteck.Splines;
using Spawn;
using UnityEngine;

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