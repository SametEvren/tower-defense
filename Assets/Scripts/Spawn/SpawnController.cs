using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
using UnityEngine.Pool;
using Utility;
using Wave_Scripts;

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
        
        [Header("Wave Points")]
        [SerializeField] private List<WavePoint> wavePoints;

        private void Awake()
        {
            SetPool();
            ServiceLocator.Add(this);
        }

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
        }

        #endregion

        public Enemy GetItemFromPool(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.AlbinoNightmare:
                    var albinoEnemy = _albinoNightmareDragonPool.Get();
                    return albinoEnemy;
                case EnemyType.BlueUsurper:
                    var blueEnemy = _blueUsurperDragonPool.Get();
                    return blueEnemy;
                case EnemyType.PurpleTerrorBringer:
                    var purpleEnemy = _purpleTerrorBringerDragonPool.Get();
                    return purpleEnemy;
                case EnemyType.RedSoulEater:
                    var redEnemy = _redSoulEaterDragonPool.Get();
                    return redEnemy;
                default:
                    var enemy = _albinoNightmareDragonPool.Get();
                    return enemy;
            }
        }

        public void ReleaseItemToPool(EnemyType enemyType, Enemy enemy)
        {
            switch (enemyType)
            {
                case EnemyType.AlbinoNightmare:
                    _albinoNightmareDragonPool.Release(enemy);
                    break;
                case EnemyType.BlueUsurper:
                    _blueUsurperDragonPool.Release(enemy);
                    break;
                case EnemyType.PurpleTerrorBringer:
                    _purpleTerrorBringerDragonPool.Release(enemy);
                    break;
                case EnemyType.RedSoulEater:
                    _redSoulEaterDragonPool.Release(enemy);
                    break;
            }
        }
    }
}