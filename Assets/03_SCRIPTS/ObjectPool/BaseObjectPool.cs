using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.ObjectPool
{
    public class BaseObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _pool;

        public BaseObjectPool(T prefab, Transform container = null, int initialSize = 0)
        {
            _prefab = prefab;
            _container = container;
            _pool = new Queue<T>();
            if (initialSize <= 0) return;
            
            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreateNewObject();
            }
        }

        public T Get()
        {
            T obj = null;
            
            if (_pool.Count > 0)
                obj = _pool.Dequeue();
            else
                obj = CreateNewObject();
            
            if (obj == null)
                obj = CreateNewObject();

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Return(T obj)
        {
            if(obj == null) return;

            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        private T CreateNewObject()
        {
            T newObj = Object.Instantiate(_prefab, _container);
            return newObj;
        }
    }
}