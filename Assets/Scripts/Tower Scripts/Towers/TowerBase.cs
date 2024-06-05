using Enemy_Scripts;
using Tower_Scripts.Projectiles;
using UnityEngine;

namespace Tower_Scripts.Towers
{
    public abstract class TowerBase<TAmmo> : ITower where TAmmo : Projectile
    {
        protected abstract ProjectileInformation ProjectileInformation { get; }
        protected override void ChooseTarget()
        {
            Enemy targetEnemy = null;
            
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range/2f, enemyLayerMask);

            float closestTargetDistance = float.MaxValue;
            
            foreach (var hitCollider in hitColliders)
            {
                var distance = Vector3.Distance(hitCollider.transform.position, transform.position);
                if (distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    targetEnemy = hitCollider.GetComponent<Enemy>();
                }
            }
            
            EnemyTarget = targetEnemy;
        }

        protected override void ShootAtTarget()
        {
            if(EnemyTarget == null)
                return;

            LookAtTarget();
            var projectile = Instantiate(towerConfig.projectile, projectileSpawnPosition.position, Quaternion.identity) as TAmmo;
            //TODO: Pool stuff

            projectile.InitializeProjectile(ProjectileInformation);
        }

        private void LookAtTarget()
        {
            Vector3 targetPosition = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z);

            transform.LookAt(targetPosition);
        }
    }
}