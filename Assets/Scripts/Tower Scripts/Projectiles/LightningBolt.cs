using System.Collections.Generic;
using DG.Tweening;
using Enemy_Scripts;
using UnityEngine;
using Utility;

namespace Tower_Scripts.Projectiles
{
    public class LightningBolt : Projectile
    {
        private int _currentChain = 0;
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
            _currentChain++;

            if (_currentChain >= ProjectileInformation.chainLength)
            {
                Destroy(gameObject);
                return;
            }
            
            ProjectileInformation.target = CheckForNewEnemy(enemy).transform.position;
            MoveTowardsTarget();
        }

        private Enemy CheckForNewEnemy(Enemy enemy)
        {
            List<Enemy> affectedEnemies = new();
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ProjectileInformation.projectileStats.areaOfEffect, enemyLayerMask);
            
            foreach (var hitCollider in hitColliders)
            {
                var targetedEnemy = hitCollider.GetComponent<Enemy>();
                if (targetedEnemy == enemy) continue;
                affectedEnemies.Add(enemy);
            }

            return affectedEnemies.PickRandomItem();
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
            enemy.AddStatus(StatusEffect.Shock, 0.1f);
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