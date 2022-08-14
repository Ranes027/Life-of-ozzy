using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Utils;
using LifeOfOzzy.Model;

namespace LifeOfOzzy.Components
{
    public class CollectorComponent : MonoBehaviour, IAddInInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();
        
        public void  AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) {Value = value});
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach(var inventoryItemData in _items)
            {
                session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);
            }

            _items.Clear();
        }
    }
}

