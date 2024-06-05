
using UnityEngine;

namespace Tower_Scripts.Projectiles
{
    public struct ProjectileStats 
    {
        public float speed;
        public float damage;
        public float areaOfEffect; // 0 = single target.

        public ProjectileStats(float speed, float damage, float areaOfEffect)
        {
            this.speed = speed;
            this.damage = damage;
            this.areaOfEffect = areaOfEffect;
        }
    }


    public struct ProjectileInformation
    {
        public ProjectileStats projectileStats;
        public Vector3 target;
        public float arcHeight;

        public ProjectileInformation(ProjectileStats stats, Vector3 targetPosition, float arcHeight)
        {
            projectileStats = stats;
            target = targetPosition;
            this.arcHeight = arcHeight;
        }
    }
}