using System.Collections.Generic;
using DG.Tweening;
using Enemy_Scripts;
using UnityEngine;

namespace Tower_Scripts.Projectiles
{
    public class Bolt : Projectile
    {
        private Sequence sequence;
        public override void InitializeProjectile(ProjectileInformation projectileInfo)
        {
            base.InitializeProjectile(projectileInfo);
            MoveTowardsTarget();
        }
        
        protected override void HandleTargetHit(Collider hitCollider)
        {
            var enemy = GetEnemy(hitCollider);
            DamageEnemy(enemy);
            Destroy(gameObject);
        }

        private Enemy GetEnemy(Collider hitCollider)
        {
            return hitCollider.CompareTag("Enemy") 
                ? hitCollider.GetComponent<Enemy>() 
                : null;
        }

        private void DamageEnemy(Enemy enemy)
        {
            if (enemy == null) return;
            enemy.TakeDamage(ProjectileInformation.projectileStats.damage);
        }

        private void MoveTowardsTarget()
        {
            var travelDuration = Vector3.Distance(transform.position, ProjectileInformation.target) / ProjectileInformation.projectileStats.speed;

            sequence = DOTween.Sequence()
                .Append(transform.DOMove(ProjectileInformation.target, travelDuration));
        }

        private void OnDestroy()
        {
            sequence.Kill();
        }
    }
}