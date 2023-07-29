
using System;
using System.Collections.Generic;
using UnityEngine;
namespace ArcheroClone.pool
{
    public interface IPool<T>
    {
        public void Return(T item);

        public T Request();
    }

    public class Pool<T> : IPool<T> where T : MonoBehaviour
    {
        public readonly Stack<T> allItem = new();
        public readonly T item;
        public Action<T> ReturnObject, RequestObject;
        public Pool(T prefab)
        {
            item = prefab;
        }
        public T Request()
        {
            T item = allItem.Count > 0 ? allItem.Pop() : GameObject.Instantiate(this.item);
            RequestObject(item);
            return item;
        }

        public void Return(T item)
        {
            allItem.Push(item);
            ReturnObject(item);
        }
    }

    public class PoolMain
    {
        public Pool<T> CreatePool<T>(T item) where T : MonoBehaviour
        {
            Pool<T> pool = new(item);
            void PoolReturnObject(T item)
            {
                item.gameObject.SetActive(false);
            }

            pool.ReturnObject = PoolReturnObject;

            void PoolRequestObject(T item)
            {
                item.gameObject.SetActive(true);
            }

            pool.RequestObject = PoolRequestObject;
            return pool;
        }
    }
}