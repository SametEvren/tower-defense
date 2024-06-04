using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask towerLayer; 
    private GameObject _currentPlacingTower;
    private float weaponPlacementOffset = 3.3f;
    private Vector3 weaponPlacementRotation = new (0, 0, 0);
    private void Update()
    {
        if (_currentPlacingTower != null)
        {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 2000f, towerLayer))
            {
                _currentPlacingTower.transform.position = hitInfo.transform.position + Vector3.up * weaponPlacementOffset;
                _currentPlacingTower.transform.localRotation = Quaternion.Euler(weaponPlacementRotation);
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