using System.Collections.Generic;
using System.Linq;
using Tower_Scripts;
using UnityEngine;

namespace UI
{
    public class TowerSelectionGUI : MonoBehaviour
    {
        [SerializeField] private List<TowerSelectionItem> selectableTowers;
        [SerializeField] private List<TowerConfig> towerConfigs;
        private List<TowerType> _activeTowers = new();

        void HandleUnlockedTower(TowerType towerType)
        {
            if (_activeTowers.Contains(towerType))
                return;
            
            _activeTowers.Add(towerType);

            RenderGUI();
        }

        void RenderGUI()
        {
            for (var i = 0; i < _activeTowers.Count; i++)
            {
                var activeTower = _activeTowers[i];
                var towerSelection = selectableTowers[i];
                var config = towerConfigs.FirstOrDefault(t => t.towerType == activeTower);
                towerSelection.Render(config);
            }
        }
    }
}
