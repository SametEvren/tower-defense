using System;
using UnityEngine;
using Utility;

namespace Player_Scripts
{
    public class PlayerStatController : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;
        public event Action<int> onGoldChanged;

        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        public bool CanAfford(int amount)
        {
            return playerStats.gold >= amount;
        }
        
        public void IncreaseGold(int amount)
        {
            playerStats.gold += amount;
            onGoldChanged?.Invoke(playerStats.gold);
        }

        public void DecreaseGold(int amount)
        {
            playerStats.gold -= amount;
            onGoldChanged?.Invoke(playerStats.gold);
        }
        
    }
}