using Gameplay_Scripts;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private GameplayController GameplayController => ServiceLocator.Get<GameplayController>();

        public GameObject endGameUI;
        private void Start()
        {
            text.text = GameplayController.CurretHealth.ToString();
            GameplayController.OnHealthChanged += UpdateHealth;
        }

        private void UpdateHealth(int health)
        {
            if (health <= 0)
            {
                endGameUI.SetActive(true);
            }
            
            text.text = health.ToString();
        }
    }
}