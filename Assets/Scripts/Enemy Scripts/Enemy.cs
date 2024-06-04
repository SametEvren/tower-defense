using System;
using UnityEngine;

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
        
        [SerializeField] private EnemyStats enemyStats;

        #region Actions

        public Action<float> EnemyHealthChanged;

        #endregion

        protected void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        protected void Start()
        {
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
            _enemyMovement.moveSpeed = Speed;
            _enemyMovement._splineFollower.followSpeed = _enemyMovement.moveSpeed;
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