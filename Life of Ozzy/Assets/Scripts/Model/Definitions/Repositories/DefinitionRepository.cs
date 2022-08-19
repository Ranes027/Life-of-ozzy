using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    public class DefinitionRepository<TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] _collection;

        public TDefType Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return default;

            foreach (var itemDefinition in _collection)
            {
                if (itemDefinition.Id == id)
                    return itemDefinition;
            }

            return default;
        }

        public TDefType[] All => new List<TDefType>(_collection).ToArray();
    }

}
