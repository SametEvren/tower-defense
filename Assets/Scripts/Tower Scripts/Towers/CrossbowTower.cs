using Tower_Scripts.Projectiles;

namespace Tower_Scripts.Towers
{
    public class CrossbowTower : TowerBase<Bolt>
    {
        protected override ProjectileInformation ProjectileInformation => new(
            new ProjectileStats(Speed, Damage, AreaOfEffect),
            EnemyTarget.transform.position, 0);
    }
}