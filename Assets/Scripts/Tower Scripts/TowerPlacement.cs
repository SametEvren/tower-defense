using UnityEngine;

namespace Tower_Scripts
{
    public class TowerPlacement : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private LayerMask towerLayer; 
        private GameObject _currentPlacingTower;
        private float _weaponPlacementOffset = 3.3f;
        private Vector3 _weaponPlacementRotation = new (0, 0, 0);
        private void Update()
        {
            if (_currentPlacingTower != null)
            {
                Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(camRay, out RaycastHit hitInfo, 2000f, towerLayer))
                {
                    _currentPlacingTower.transform.position = hitInfo.transform.position + Vector3.up * _weaponPlacementOffset;
                    _currentPlacingTower.transform.localRotation = Quaternion.Euler(_weaponPlacementRotation);
                    _currentPlacingTower.transform.parent = hitInfo.transform;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _currentPlacingTower = null;
                }
            }
        }

        public void SetTowerToPlace(GameObject tower)
        {
            _currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        }
    }
}