using System;
using Tower_Scripts.Projectiles;
using UnityEngine;

namespace Tower_Scripts.Towers
{
    public class TeslaTower : TowerBase<LightningBolt>
    {
        [SerializeField] private int chainLength;
        protected override ProjectileInformation ProjectileInformation => new(
            new ProjectileStats(Speed, Damage, AreaOfEffect),
            EnemyTarget.transform.position, 0, chainLength);
    }
}