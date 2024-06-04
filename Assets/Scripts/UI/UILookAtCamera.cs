using UnityEngine;
using Utility;

namespace UI
{
    public class UILookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;
        private UIController _uiController;
        
        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Start()
        {
            _uiController = ServiceLocator.Get<UIController>();
            transform.SetParent(_uiController.worldSpaceCanvas.transform);
        }

        private void LateUpdate()
        {
            var rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}