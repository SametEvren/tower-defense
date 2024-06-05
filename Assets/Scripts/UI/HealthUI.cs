using Gameplay_Scripts;
using Player_Scripts;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private GameplayController GameplayController => ServiceLocator.Get<GameplayController>();

        private void Start()
        {
            GameplayController.OnHealthChanged += UpdateGoldCount;
        }

        private void UpdateGoldCount(int health)
        {
            text.text = health.ToString();
        }
    }
}