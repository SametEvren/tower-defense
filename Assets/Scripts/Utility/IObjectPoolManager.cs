using Enemy_Scripts;
using UnityEngine.Pool;

namespace Utility
{
    public interface IObjectPoolManager<T> where T : class
    {
        public Enemy GetItemFromPool(ObjectPool<T> item);
        public void ReleaseItemToPool(T item);
    }
}