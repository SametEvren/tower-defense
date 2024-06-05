using Tower_Scripts.Towers;
using UI;
using UnityEngine;

namespace Utility
{
    public class PlayerTowerSelectionManager : MonoBehaviour
    {
        public LayerMask towerLayerMask;

        private UpgradeTheButton _previousUpgradeButton;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, towerLayerMask))
                {
                    var tower = hit.transform.GetComponent<ITower>();
                    var upgradeButton = tower.upgradeButton;


                    _previousUpgradeButton?.Hide();
                    _previousUpgradeButton = upgradeButton;
                    upgradeButton.Render();
                }
            }
        }
    }
}