using System;
using DG.Tweening;
using Player_Scripts;
using Tower_Scripts.Towers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UpgradeTheButton : MonoBehaviour
    {
        private PlayerStatController StatsController => ServiceLocator.Get<PlayerStatController>();
        
        [SerializeField] private ITower parentTower;
        private Button Button => GetComponent<Button>();
        
        public void DestroyUpgradeButton()
        {
            Hide(true);
        }

        public void Hide(bool destroy = false)
        {
            if (this == null) return;
            Button.interactable = false;
            transform.DOScale(Vector3.zero, 0.1f)
                .OnComplete(() =>
                {
                    if (destroy) 
                        Destroy(gameObject); 
                    else 
                        gameObject.SetActive(false);
                });
        }

        public void Render()
        {
            transform.localScale = Vector3.zero;
            Button.interactable = !parentTower.IsMaxLevel && StatsController.CanAfford(parentTower.NextLevelCost);
            gameObject.SetActive(true);
            transform.DOScale(0.05f, 0.2f);
        }
    }
}
