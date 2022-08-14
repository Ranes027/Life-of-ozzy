using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Utils
{
    public class Pool : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<PoolItem>> _items = new Dictionary<int, Queue<PoolItem>>();

        private static Pool _instance;

        public static Pool instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("---MAIN_POOL---");
                    _instance = go.AddComponent<Pool>();
                }
                return _instance;
            }
        }

        public GameObject Get(GameObject go, Vector3 position)
        {
            var id = go.GetInstanceID();
            var queue = RequireQueue(id);

            if (queue.Count > 0)
            {
                var pooledItem = queue.Dequeue();
                pooledItem.transform.position = position;
                pooledItem.gameObject.SetActive(true);
                pooledItem.Restart();
                return pooledItem.gameObject;
            }

            var instance = SpawnUtils.Spawn(go, position, gameObject.name);
            var poolItem = instance.GetComponent<PoolItem>();
            poolItem.Retain(id, this);

            return instance;
        }

        private Queue<PoolItem> RequireQueue(int id)
        {
            if (!_items.TryGetValue(id, out var queue))
            {
                queue = new Queue<PoolItem>();
                _items.Add(id, queue);
            }

            return queue;
        }

        public void Release(int id, PoolItem poolItem)
        {
            var queue = RequireQueue(id);
            queue.Enqueue(poolItem);

            poolItem.gameObject.SetActive(false);
        }
    }
}

