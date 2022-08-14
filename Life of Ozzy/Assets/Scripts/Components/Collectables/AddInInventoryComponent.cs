using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Components
{
    public class AddInInventoryComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<IAddInInventory>();
            hero?.AddInInventory(_id,_count);

        }
    }

}
