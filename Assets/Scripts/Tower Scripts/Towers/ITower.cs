using Enemy_Scripts;
using Tower_Scripts.Projectiles;
using UI;
using UnityEngine;

namespace Tower_Scripts.Towers
{
    public abstract class ITower : MonoBehaviour
    {
        [SerializeField] protected TowerConfig towerConfig;
        public TowerModifiers TowerModifiers = new() { damageModifier = 1, fireRateModifier = 1, rangeModifier = 1 };
        public int Level { get; private set; }

        public bool IsMaxLevel => Level >= 2;
        public int NextLevelCost => towerConfig.upgradeCosts[Level];

        public float Speed => towerConfig.projectileSpeed;
        public float Range => towerConfig.rangeUpgrades[Level] * TowerModifiers.rangeModifier;
        public float Damage => towerConfig.damageUpgrades[Level] * TowerModifiers.damageModifier;
        public float FireRate => towerConfig.fireRateUpgrades[Level] * TowerModifiers.fireRateModifier;
        public float AreaOfEffect => towerConfig.areaOfEffect[Level];

        private bool _isActive;
        private float _remainingCooldown;
        protected Enemy EnemyTarget;

        public UpgradeTheButton upgradeButton;
        
        [SerializeField] protected Transform projectileSpawnPosition;
        
        private void Update()
        {
            if(!_isActive) return;

            _remainingCooldown -= Time.deltaTime;
            
            if(_remainingCooldown > 0) return;
            
            ChooseTarget();
            ShootAtTarget();
            _remainingCooldown = FireRate;
        }

        public void UpgradeTower()
        {
            Level++;
            var parent = transform.parent;
            var tower = Instantiate(towerConfig.towerStages[Level], Vector3.zero, Quaternion.identity, parent);
            tower.transform.localPosition = Vector3.up * TowerPlacement.WeaponPlacementOffset;
            tower.transform.localRotation = Quaternion.Euler(TowerPlacement.WeaponPlacementRotation);
            tower.ActivateTower(Level);
            Destroy(gameObject);
            //TODO: Particle effects
        }
        
        public void ActivateTower(int level = 0)
        {
            _isActive = true;
            Level = level;
        }

        protected abstract void ChooseTarget();

        protected abstract void ShootAtTarget();
    }
}