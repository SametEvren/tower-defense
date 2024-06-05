using System;
using System.Collections;
using Gameplay_Scripts;
using UnityEngine;
using Utility;

namespace Player_Scripts
{
    public class PlayerStatController : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;

        public int CurrentGold => playerStats.gold;
        public event Action<int> OnGoldChanged;

        public int StartingHealth => playerStats.startingHealth;
        private GameplayController GameplayController => ServiceLocator.Get<GameplayController>();
        

        private void Awake()
        {
            ServiceLocator.Add(this);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            OnGoldChanged?.Invoke(playerStats.gold);
        }

        public bool CanAfford(int amount)
        {
            return playerStats.gold >= amount;
        }
        
        public void IncreaseGold(int amount)
        {
            playerStats.gold += amount;
            OnGoldChanged?.Invoke(playerStats.gold);
        }

        public void DecreaseGold(int amount)
        {
            playerStats.gold -= amount;
            OnGoldChanged?.Invoke(playerStats.gold);
        }
        
    }
}