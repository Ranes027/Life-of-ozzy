using System;
using System.Linq;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/ThrowableItems", fileName = "ThrowableItems")]
    public class ThrowableRepository : DefinitionRepository<ThrowableDef>
    {

    }

    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}
