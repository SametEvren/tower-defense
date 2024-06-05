using UnityEngine;

namespace Tower_Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        protected ProjectileInformation ProjectileInformation;
        protected ParticleSystem ParticleEffects;

        public virtual void InitializeProjectile(ProjectileInformation info)
        {
            ProjectileInformation = info;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy") && !other.CompareTag("Ground")) return;

            HandleTargetHit();
        }

        protected abstract void HandleTargetHit();
    }
}