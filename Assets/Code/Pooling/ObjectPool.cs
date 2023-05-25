using System.Collections.Generic;
using Code.Player;
using Code.Player.Shooting.Enums;
using UnityEngine;
using Zenject;

namespace Code.Pooling
{

    public abstract class ObjectPool<T> where T : Component
    {
        private DiContainer _container;
        private Transform _holder;
        private Queue<T> _objectPool = new Queue<T>();
        private T _prefab;

        public BulletType Type { get; private set; }

        public ObjectPool(T prefab, int initialSize,DiContainer container, Transform holder, BulletType type)
        {
            _prefab = prefab;
            _container = container;
            _holder = holder;
            Type = type;
            
            for (int i = 0; i < initialSize; i++)
            {
                var obj = _container.InstantiatePrefabForComponent<T>(_prefab);
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(_holder);
                _objectPool.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            T obj;
            if (_objectPool.Count > 0)
            {
                obj = _objectPool.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = _container.InstantiatePrefabForComponent<T>(_prefab);
            }
            return obj;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_holder);
            _objectPool.Enqueue(obj);
        }
    }

}