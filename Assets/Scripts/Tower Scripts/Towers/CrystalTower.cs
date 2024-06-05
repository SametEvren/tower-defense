using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;

namespace Tower_Scripts.Towers
{
    public class CrystalTower : ITower
    {
        private List<Enemy> enemiesInEffect = new();
        protected override void ChooseTarget()
        {
            enemiesInEffect.Clear();
            
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range/2f, enemyLayerMask);
            
            foreach (var hitCollider in hitColliders)
            {
                enemiesInEffect.Add(hitCollider.GetComponent<Enemy>());
            }
        }

        protected override void ShootAtTarget()
        {
            //TODO: Particles
            foreach (var enemy in enemiesInEffect)
            {
                enemy.TakeDamage(Damage);
                enemy.AddStatus(StatusEffect.Frost, 0.3f);
            }
        }
    }
}