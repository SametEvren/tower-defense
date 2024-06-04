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
        [SerializeField] private Enemy albinoNightmareDragonPrefab;
        private const int SlotItemCapacity = 2000;
        [SerializeField] private List<WavePoint> wavePoints;

        private void Awake()
        {
            SetPool();
            ServiceLocator.Add<IObjectPoolManager<Enemy>>(this);
        }

        private void Start()
        {
            var spawnedEnemy = GetItemFromPool();
            var enemyMovement = spawnedEnemy.GetComponent<EnemyMovement>();
            enemyMovement.SetMoveFeature(wavePoints[3]);
        }

        #region SetPool

        private void SetPool()
        {
            _albinoNightmareDragonPool = CreatePool(albinoNightmareDragonPrefab, SlotItemCapacity);
        }
    
        private ObjectPool<Enemy> CreatePool(Enemy prefab, int capacity)
        {
            return new ObjectPool<Enemy>(() => Instantiate(prefab), ActionOnGet, OnPutBackInPool, defaultCapacity: capacity);
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

        public Enemy GetItemFromPool()
        {
            var enemy = _albinoNightmareDragonPool.Get();
            return enemy;
        }

        public void ReleaseItemToPool(Enemy enemy)
        {
            OnPutBackInPool(enemy);
        }
    }
}