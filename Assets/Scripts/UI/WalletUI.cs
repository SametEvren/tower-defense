using Player_Scripts;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        private PlayerStatController PlayerStatController => ServiceLocator.Get<PlayerStatController>();

        private void Start()
        {
            moneyText.text = PlayerStatController.CurrentGold.ToString();
            PlayerStatController.OnGoldChanged += UpdateGoldCount;
        }

        private void UpdateGoldCount(int gold)
        {
            moneyText.text = gold.ToString();
        }
    }
}