using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay_Scripts;
using Player_Scripts;
using Spawn;
using UnityEngine;
using Utility;

namespace Enemy_Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        private float _health;

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                {
                    OnEnemyDeath?.Invoke(this);
                    SpawnController.ReleaseItemToPool(enemyType, this);
                    PlayerStatController.IncreaseGold(50);
                }
                
                EnemyHealthChanged?.Invoke(_health);
            }
        }
        public float Damage => (enemyStats.damage) * ProgressionBuff;

        public float Speed => (enemyStats.speed * FrostDebuff * ShockDebuff) * ProgressionBuff;

        //Virtual in case of debuff resistant enemies (?)
        protected virtual float FrostDebuff => statusEffects.ContainsKey(StatusEffect.Frost) ? 0.5f : 1f;
        protected virtual float ShockDebuff => statusEffects.ContainsKey(StatusEffect.Shock) ? 0f : 1f;
        protected float ProgressionBuff => 1f;
        //TODO: ServiceLocator.Get<GameplayController>().GetProgressionMultiplier;

        public float StartHealth { get; private set; }

        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;
        private EnemyUI _enemyUI;

        private Dictionary<StatusEffect, float> statusEffects = new();

        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyStats enemyStats;

        private SpawnController SpawnController => ServiceLocator.Get<SpawnController>();
        private GameplayController GameplayController => ServiceLocator.Get<GameplayController>();
        private PlayerStatController PlayerStatController => ServiceLocator.Get<PlayerStatController>();

        #region Actions

        public event Action OnEnemySpawned; 
        public event Action<float> EnemyHealthChanged;
        public event Action<Enemy> OnEnemyDeath;

        #endregion

        protected void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        protected void OnEnable()
        {
            OnEnemySpawned?.Invoke();
            GetProperties();
            SetProperties();
            ResetMovement();
        }

        private void Update()
        {
            ReduceStatusEffectTimers(Time.deltaTime);
            CheckProgress();
        }

        private void CheckProgress()
        {
            if (_enemyMovement.ReachedTheEnd)
            {
                GameplayController.DecreaseHealth(1);
                OnEnemyDeath?.Invoke(this);
                SpawnController.ReleaseItemToPool(enemyType,this);
            }
        }

        private void ReduceStatusEffectTimers(float timePassed)
        {
            var statusesToRemove = new List<StatusEffect>();
            var statusEffectsKeys = new List<StatusEffect>(statusEffects.Keys);

            foreach (var effect in statusEffectsKeys)
            {
                statusEffects[effect] -= timePassed;
                if (statusEffects[effect] <= 0)
                {
                    statusesToRemove.Add(effect);
                }
            }

            foreach (var effect in statusesToRemove)
            {
                RemoveEffect(effect);
            }
        }

        private void RemoveEffect(StatusEffect effect)
        {
            if (!statusEffects.ContainsKey(effect)) return;
            
            statusEffects.Remove(effect);
            SetProperties();
        }

        private void GetProperties()
        {
            StartHealth = Health = enemyStats.health * ProgressionBuff;
        }

        private void ResetMovement()
        {
            _enemyMovement.ResetMovement();
        }
        
        private void SetProperties()
        {
            _enemyMovement.SetMoveSpeed(Speed);
            _enemyAttack.damageAmount = Damage;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
        }

        public abstract void Move();
        public abstract void Attack();

        public void AddStatus(StatusEffect effect, float duration)
        {
            if (statusEffects.ContainsKey(effect))
                statusEffects[effect] += duration;
            else
                statusEffects.Add(effect, duration);
        }
    }
}