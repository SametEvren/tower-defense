using Tower_Scripts.Projectiles;
using Tower_Scripts.Towers;
using UnityEngine;

namespace Tower_Scripts
{
    [CreateAssetMenu(fileName = "Tower Config", menuName = "Tower/Tower Config")]
    public class TowerConfig : ScriptableObject
    {
        public TowerType towerType;
        public TowerInterface[] towerStages;
        public Projectile projectile;
        public Sprite sprite;
        public string description;
        public float projectileSpeed;
        public float[] rangeUpgrades;
        public float[] damageUpgrades;
        public float[] fireRateUpgrades;
        public float[] areaOfEffect;
        public int baseCost;
        public int[] upgradeCosts;

        public T GetTowerPrefab<T>(int level) where T : TowerInterface
        {
            return towerStages[level] as T;
        }
    }

}