using System;
using Spawn;
using UnityEngine;
using Utility;

namespace Enemy_Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        private float _health;
        private float _damage;
        private float _speed;

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
                }
                
                EnemyHealthChanged?.Invoke(_health);
            }
        }
        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float StartHealth { get; private set; }

        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;
        private EnemyUI _enemyUI;

        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyStats enemyStats;

        private SpawnController SpawnController => ServiceLocator.Get<SpawnController>();

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
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                TakeDamage(10f);
        }

        private void GetProperties()
        {
            StartHealth = Health = enemyStats.health; //TODO: Multiply with level multiplier
            Damage = enemyStats.damage; //TODO: Multiply with level multiplier
            Speed = enemyStats.speed; //TODO: Multiply with level multiplier
        }

        private void SetProperties()
        {
            _enemyMovement.SetMoveSpeed(_speed);
            _enemyAttack.damageAmount = Damage;
        }

        private void TakeDamage(float amount)
        {
            Health -= amount;
        }

        public abstract void Move();
        public abstract void Attack();
    }
}