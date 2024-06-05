using Dreamteck.Splines;
using UnityEngine;
using Wave_Scripts;

namespace Enemy_Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed;
        private SplineFollower _splineFollower;

        public void SetMoveSpeed(float speed)
        {
            _splineFollower ??= GetComponent<SplineFollower>();
            moveSpeed = speed;
            _splineFollower.followSpeed = moveSpeed;
        }
        
        public void SetMoveFeature(WavePoint wavePoint)
        {
            transform.position = wavePoint.transform.position;
            _splineFollower.spline = wavePoint.splineComputer;
        }

        public void ResetMovement()
        {
            _splineFollower ??= GetComponent<SplineFollower>();
            _splineFollower.SetPercent(0.0);
        }
    }
}