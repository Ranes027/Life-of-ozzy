using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntObservableProperty Hp = new IntObservableProperty();
        public PerksData Perks = new PerksData();
        public StatsData Stats = new StatsData();
        public ShopData Shops = new ShopData();


        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}

