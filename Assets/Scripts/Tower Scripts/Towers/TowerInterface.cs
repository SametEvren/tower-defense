using Enemy_Scripts;
using Tower_Scripts.Projectiles;
using UnityEngine;

namespace Tower_Scripts.Towers
{
    public abstract class TowerInterface : MonoBehaviour
    {
        [SerializeField] protected TowerConfig towerConfig;
        public TowerModifiers TowerModifiers = new TowerModifiers() { damageModifier = 1, fireRateModifier = 1, rangeModifier = 1 };
        public int Level { get; private set; }

        public float Speed => towerConfig.projectileSpeed;
        public float Range => towerConfig.rangeUpgrades[Level] * TowerModifiers.rangeModifier;
        public float Damage => towerConfig.damageUpgrades[Level] * TowerModifiers.damageModifier;
        public float FireRate => towerConfig.fireRateUpgrades[Level] * TowerModifiers.fireRateModifier;
        public float AreaOfEffect => towerConfig.areaOfEffect[Level];

        private bool _isActive;
        private float _remainingCooldown;
        protected Enemy EnemyTarget;

        [SerializeField] protected Projectile projectilePrefab;
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

        public virtual void UpgradeTower()
        {
            Level++;
        }
        
        public void ActivateTower()
        {
            _isActive = true;
        }

        protected abstract void ChooseTarget();

        protected abstract void ShootAtTarget();
    }
}