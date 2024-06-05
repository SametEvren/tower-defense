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
        private TowerInterface _currentPlacingTowerInterface;
        private TowerConfig _currentSelectedConfig;
        private float _weaponPlacementOffset = 3.3f;
        private Vector3 _weaponPlacementRotation = new (0, 0, 0);

        private PlayerStatController PlayerStatsController => ServiceLocator.Get<PlayerStatController>();
        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        private void Update()
        {
            if (_currentPlacingTowerInterface != null)
            {
                RenderTowerPlacement();
            }
        }

        void RenderTowerPlacement()
        {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 2000f, towerLayer))
            {
                _currentPlacingTowerInterface.transform.position = hitInfo.transform.position + Vector3.up * _weaponPlacementOffset;
                _currentPlacingTowerInterface.transform.localRotation = Quaternion.Euler(_weaponPlacementRotation);
                _currentPlacingTowerInterface.transform.parent = hitInfo.transform;
            }

            if (Input.GetMouseButtonDown(0))
            {
                BuyTower();
                _currentPlacingTowerInterface = null;
                _currentSelectedConfig = null;
            }
        }

        private void BuyTower()
        {
            _currentPlacingTowerInterface.ActivateTower();
            PlayerStatsController.DecreaseGold(_currentSelectedConfig.baseCost);
        }

        public void SetTowerToPlace(TowerConfig towerConfig)
        {
            var tower = Instantiate(towerConfig.towerStages[0], Vector3.zero, Quaternion.identity);
            _currentPlacingTowerInterface = tower;
            _currentSelectedConfig = towerConfig;
        }
    }
}