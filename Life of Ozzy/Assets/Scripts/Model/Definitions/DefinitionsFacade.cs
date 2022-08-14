using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/DefinitionsFacade", fileName = "DefinitionsFacade")]
    public class DefinitionsFacade : ScriptableObject
    {
        [SerializeField] private ItemRepository _items;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PotionRepository _potions;
        [SerializeField] private PlayerDefinition _player;
        [SerializeField] private PerkRepository _perks;
        [SerializeField] private ShopRepository _shop;

        public ItemRepository Items => _items;
        public ThrowableRepository ThrowableItems => _throwableItems;
        public PotionRepository Potions => _potions;
        public PlayerDefinition Player => _player;
        public PerkRepository Perks => _perks;
        public ShopRepository Shop => _shop;

        private static DefinitionsFacade _instance;
        public static DefinitionsFacade I => _instance == null ? LoadDefinitions() : _instance;

        private static DefinitionsFacade LoadDefinitions()
        {
            return _instance = Resources.Load<DefinitionsFacade>("DefinitionsFacade");
        }
    }
}
