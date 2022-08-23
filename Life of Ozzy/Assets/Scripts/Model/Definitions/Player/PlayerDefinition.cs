using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/PlayerDefinition", fileName = "PlayerDefinition")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        [SerializeField] private StatDefinition[] _stats;

        public int InventorySize => _inventorySize;

        public StatDefinition[] Stats => _stats;

        public StatDefinition GetStat(StatId id)
        {
            foreach (var statDef in _stats)
            {
                if (statDef.Id == id)
                    return statDef;
            }
            
            return default;
        }
    }

}
