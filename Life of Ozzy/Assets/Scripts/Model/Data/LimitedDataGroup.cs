using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    public class LimitedDataGroup<TDataType, TItemType>
        where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        private readonly List<TItemType> CreatedItems = new List<TItemType>();
        private readonly TItemType _prefab;
        private readonly Transform _container;

        public LimitedDataGroup(TItemType prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void SetLimitedData(IList<TDataType> data, int minItems, int startIndex)
        {
            // Create required items
            for (var i = CreatedItems.Count; i < minItems; i++)
            {
                var item = Object.Instantiate(_prefab, _container);
                CreatedItems.Add(item);
            }

            // Update data & activate
            for (var i = startIndex; i < data.Count; i++)
            {
                CreatedItems[i].SetData(data[i], i);
                CreatedItems[i].gameObject.SetActive(true);
            }

            // Hide unused items
            for (var i = data.Count; i < CreatedItems.Count; i++)
            {
                CreatedItems[i].gameObject.SetActive(false);
            }
        }
    }
}

