using Player_Scripts;
using Tower_Scripts.Towers;
using UnityEngine;
using Utility;

namespace Tower_Scripts
{
    public class TowerPlacement : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private LayerMask towerLayer; 
        private ITower _currentPlacingTower;
        private TowerConfig _currentSelectedConfig;
        public const float WeaponPlacementOffset = 3.3f;
        public static Vector3 WeaponPlacementRotation = new (0, 0, 0);

        private PlayerStatController PlayerStatsController => ServiceLocator.Get<PlayerStatController>();
        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        private void Update()
        {
            if (_currentPlacingTower != null)
            {
                RenderTowerPlacement();
            }
        }

        void RenderTowerPlacement()
        {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 2000f, towerLayer))
            {
                _currentPlacingTower.transform.position = hitInfo.transform.position + Vector3.up * WeaponPlacementOffset;
                _currentPlacingTower.transform.localRotation = Quaternion.Euler(WeaponPlacementRotation);
                _currentPlacingTower.transform.parent = hitInfo.transform;
            }

            if (Input.GetMouseButtonDown(0))
            {
                BuyTower();
                _currentPlacingTower = null;
                _currentSelectedConfig = null;
            }
        }

        private void BuyTower()
        {
            _currentPlacingTower.ActivateTower();
            PlayerStatsController.DecreaseGold(_currentSelectedConfig.baseCost);
        }

        public void SetTowerToPlace(TowerConfig towerConfig)
        {
            var tower = Instantiate(towerConfig.towerStages[0], Vector3.zero, Quaternion.identity);
            _currentPlacingTower = tower;
            _currentSelectedConfig = towerConfig;
        }
    }
}