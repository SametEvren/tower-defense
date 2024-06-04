using Enemy_Scripts;

namespace Utility
{
    public interface IObjectPoolManager<T>  
    {
        public T GetItemFromPool(EnemyType enemyType);
        public void ReleaseItemToPool(T item);
    }
}