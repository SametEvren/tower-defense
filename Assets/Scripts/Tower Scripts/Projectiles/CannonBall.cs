using System;
using System.Collections.Generic;
using DG.Tweening;
using Enemy_Scripts;
using UnityEngine;

namespace Tower_Scripts.Projectiles
{
    public class CannonBall : Projectile
    {
        private Sequence sequence;
        public override void InitializeProjectile(ProjectileInformation projectileInfo)
        {
            base.InitializeProjectile(projectileInfo);
            MoveTowardsTarget();
        }
        
        protected override void HandleTargetHit()
        {
            var affectedEnemies = GetAffectedEnemies();
            PlayExplosionFX();
            DamageEnemies(affectedEnemies);
            Destroy(gameObject);
            //TODO: Handle Particle Effects.
        }

        private void PlayExplosionFX()
        {
            //ParticleEffects.Play();
        }

        private void DamageEnemies(List<Enemy> affectedEnemies)
        {
            foreach (var enemy in affectedEnemies)
            {
                enemy.TakeDamage(ProjectileInformation.projectileStats.damage);
            }
        }

        private void MoveTowardsTarget()
        {
            var travelDuration = Vector3.Distance(transform.position, ProjectileInformation.target) / ProjectileInformation.projectileStats.speed;

            sequence = DOTween.Sequence()
                .Append(transform.DOMoveX(ProjectileInformation.target.x, travelDuration))
                .Join(transform.DOMoveZ(ProjectileInformation.target.z,travelDuration))
                .Join(transform.DOMoveY(transform.position.y + ProjectileInformation.arcHeight, travelDuration/2))
                .Insert(travelDuration/2, transform.DOMoveY(ProjectileInformation.target.y,travelDuration/2)).OnComplete(
                    () =>
                    {
                        Destroy(gameObject);
                    });
        }
        
        private List<Enemy> GetAffectedEnemies()
        {
            List<Enemy> affectedEnemies = new();
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ProjectileInformation.projectileStats.areaOfEffect, enemyLayerMask);
            
            foreach (var hitCollider in hitColliders)
            {
                affectedEnemies.Add(hitCollider.GetComponent<Enemy>());
            }

            return affectedEnemies;
        }

        private void OnDestroy()
        {
            sequence.Kill();
        }
    }
}