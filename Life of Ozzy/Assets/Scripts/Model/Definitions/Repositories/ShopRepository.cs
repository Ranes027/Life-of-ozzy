using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/Shops", fileName = "Shop")]
    public class ShopRepository : DefinitionRepository<ShopDefinition>
    {

    }

    [Serializable]
    public struct ShopDefinition : IHaveId
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private string _info;
        [SerializeField] private int _amount;
        [SerializeField] private ItemWithCount _price;

        public string Id => _id;
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Info => _info;
        public int Amount => _amount;
        public ItemWithCount Price => _price;
    }

}
