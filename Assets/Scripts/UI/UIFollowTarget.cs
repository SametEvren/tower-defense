using UnityEngine;

namespace UI
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] public Transform target;
        [SerializeField] private Vector3 offset;

        private void Update()
        {
            transform.position = target.position + offset;
        }
    }
}