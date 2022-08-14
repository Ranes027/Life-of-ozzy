using System;
using System.Linq;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/Items", fileName = "Items")]
    public class ItemRepository : DefinitionRepository<ItemDefinition>
    {

#if UNITY_EDITOR
        public ItemDefinition[] ItemsForEditor => _collection;
#endif
    }

    [Serializable]
    public struct ItemDefinition : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;

        public string Id => _id;
        public Sprite Icon => _icon;

        public bool IsVoid => string.IsNullOrEmpty(_id);

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag);
        }
    }
}
