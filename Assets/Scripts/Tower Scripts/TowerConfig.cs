using UnityEngine;

namespace Tower_Scripts
{
    [CreateAssetMenu(fileName = "Tower Config", menuName = "Tower/Tower Config")]
    public class TowerConfig : ScriptableObject
    {
        public GameObject projectile;
        public Sprite sprite;
        public string description;
        public float range;
        public float damage;
        public float fireRate;
        public DamageType damageType;
        public int cost;
    }

    public enum DamageType
    {
        Area,
        Single
    }
}