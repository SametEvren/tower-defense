using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace Spawn
{
    public class SpawnController : MonoBehaviour, IObjectPoolManager<Enemy>
    {
        private ObjectPool<Enemy> _albinoNightmareDragonPool;
        private ObjectPool<Enemy> _blueUsurperDragonPool;
        private ObjectPool<Enemy> _purpleTerrorBringerDragonPool;
        private ObjectPool<Enemy> _redSoulEaterDragonPool;
        
        [Header("Dragon Prefabs")]
        [SerializeField] private Enemy albinoNightmareDragonPrefab;
        [SerializeField] private Enemy blueUsurperDragonPrefab;
        [SerializeField] private Enemy purpleTerrorBringerDragonPrefab;
        [SerializeField] private Enemy redSoulEaterDragonPrefab;

        [Header("Dragon Parents")]
        [SerializeField] private Transform albinoNightmareDragonParent;
        [SerializeField] private Transform blueUsurperDragonParent;
        [SerializeField] private Transform purpleTerrorBringerDragonParent;
        [SerializeField] private Transform redSoulEaterDragonParent;
        
        private const int SlotItemCapacity = 2000;
        
        [SerializeField] private List<WavePoint> wavePoints;

        private void Awake()
        {
            SetPool();
            ServiceLocator.Add(this);
        }

        // private void Start()
        // {
        //     var spawnedEnemy = GetItemFromPool();
        //     var enemyMovement = spawnedEnemy.GetComponent<EnemyMovement>();
        //     enemyMovement.SetMoveFeature(wavePoints[3]);
        // }

        #region SetPool

        private void SetPool()
        {
            _albinoNightmareDragonPool = CreatePool(albinoNightmareDragonPrefab, albinoNightmareDragonParent, SlotItemCapacity);
            _blueUsurperDragonPool = CreatePool(blueUsurperDragonPrefab, blueUsurperDragonParent, SlotItemCapacity);
            _purpleTerrorBringerDragonPool = CreatePool(purpleTerrorBringerDragonPrefab, purpleTerrorBringerDragonParent, SlotItemCapacity);
            _redSoulEaterDragonPool = CreatePool(redSoulEaterDragonPrefab, redSoulEaterDragonParent, SlotItemCapacity);
        }
    
        private ObjectPool<Enemy> CreatePool(Enemy prefab, Transform parent, int capacity)
        {
            return new ObjectPool<Enemy>(() => Instantiate(prefab,parent), ActionOnGet, OnPutBackInPool, defaultCapacity: capacity);
        }

        private void ActionOnGet(Enemy obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnPutBackInPool(Enemy obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(null);
        }

        #endregion

        public Enemy GetItemFromPool(ObjectPool<Enemy> enemyPool)
        {
            var enemy = enemyPool.Get();
            return enemy;
        }

        public void ReleaseItemToPool(Enemy enemy)
        {
            OnPutBackInPool(enemy);
        }
        
        public void GenerateSpawn(EnemyType enemyType, int count, int wavePointId)
        {
            ObjectPool<Enemy> enemyPoolFromSpawn;
            
            switch (enemyType)
            {
                case EnemyType.AlbinoNightmare:
                    enemyPoolFromSpawn = _albinoNightmareDragonPool;
                    break;
                case EnemyType.BlueUsurper:
                    enemyPoolFromSpawn = _blueUsurperDragonPool;
                    break;
                case EnemyType.PurpleTerrorBringer:
                    enemyPoolFromSpawn = _purpleTerrorBringerDragonPool;
                    break;
                case EnemyType.RedSoulEater:
                    enemyPoolFromSpawn = _redSoulEaterDragonPool;
                    break;
                default:
                    enemyPoolFromSpawn = _albinoNightmareDragonPool;
                    break;
            }

            var spawnedEnemy = GetItemFromPool(enemyPoolFromSpawn);
            SetEnemy(spawnedEnemy, wavePointId);
        }

        private void SetEnemy(Enemy enemy,int wavePointId)
        {
            var enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.SetMoveFeature(wavePoints[wavePointId]);
        }
    }
}