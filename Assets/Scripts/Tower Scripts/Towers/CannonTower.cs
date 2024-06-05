using Tower_Scripts.Projectiles;

namespace Tower_Scripts.Towers
{
    public class CannonTower : TowerBase<CannonBall>
    {
        protected override ProjectileInformation ProjectileInformation => new(
            new ProjectileStats(Speed, Damage, AreaOfEffect),
            EnemyTarget.transform.position, 0);
    }
}