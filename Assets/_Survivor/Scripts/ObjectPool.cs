using System.Collections.Generic;
using UnityEngine;

namespace _Survivor.Scripts
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> _poolQueue = new Queue<T>();
        private T _prefab;
        private Transform _parentTransform;

        public ObjectPool(T prefab, int initialCapacity, Transform parentTransform = null)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            for (int i = 0; i < initialCapacity; i++)
            {
                var obj = Object.Instantiate(_prefab, _parentTransform);
                obj.gameObject.SetActive(false);
                _poolQueue.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (_poolQueue.Count > 0)
            {
                var obj = _poolQueue.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Object.Instantiate(_prefab, _parentTransform);
                return newObj;
            }
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _poolQueue.Enqueue(obj);
        }
    }
}