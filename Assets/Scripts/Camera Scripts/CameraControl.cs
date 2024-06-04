using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Camera_Scripts
{
    public class CameraControl : MonoBehaviour
    {
        public float panSpeed;

        public Slider zoomSlider;
        public float minZoom;
        public float maxZoom;

        public Vector3 minBounds; 
        public Vector3 maxBounds;

        private Camera _cam;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        private void Start()
        {
            zoomSlider.onValueChanged.AddListener(OnZoomValueChanged);
        }

        void Update()
        {
            if (!IsPointerOverUIElement())
            {
                MoveTheCamera();
            }
        }

        void MoveTheCamera()
        {
            Vector3 pos = transform.position;

            if (Input.GetMouseButton(0))
            {
                float moveX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
                float moveY = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
                pos += Vector3.right * moveX;
                pos += Vector3.forward * moveY;

                pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
                pos.z = Mathf.Clamp(pos.z, minBounds.z, maxBounds.z);
            }

            transform.position = pos;
        }

        void OnZoomValueChanged(float value)
        {
            _cam.fieldOfView = Mathf.Lerp(minZoom, maxZoom, value);
        }

        private void OnDestroy()
        {
            zoomSlider.onValueChanged.RemoveListener(OnZoomValueChanged);
        }

        private bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}