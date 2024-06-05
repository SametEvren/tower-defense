using Player_Scripts;
using Tower_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class TowerSelectionItem : MonoBehaviour
    {
        public Image image;
        
        [SerializeField] private TowerConfig towerConfig;
        private PlayerStatController PlayerStatController => ServiceLocator.Get<PlayerStatController>();

        private Button button;

        private TowerPlacement TowerPlacement => ServiceLocator.Get<TowerPlacement>();

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            PlayerStatController.onGoldChanged += HandleGoldChanged;
        }

        private void HandleGoldChanged(int gold)
        {
            if (gold < towerConfig.baseCost)
            {
                RenderAsInactive();
            }
            else
            {
                RenderAsActive();
            }
        }

        private void RenderAsInactive()
        {
            button.interactable = false;
        }

        private void RenderAsActive()
        {
            button.interactable = true;
        }

        public void Render(TowerConfig config)
        {
            image.sprite = config.sprite;
            gameObject.SetActive(true);
        }

        public void HandleSelection()
        {
            TowerPlacement.SetTowerToPlace(towerConfig);
        }
    }
}