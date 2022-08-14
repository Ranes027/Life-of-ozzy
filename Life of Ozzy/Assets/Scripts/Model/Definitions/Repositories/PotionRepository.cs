using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/Potions", fileName = "Potions")]
    public class PotionRepository : DefinitionRepository<PotionDefinition>
    {

    }

    [Serializable]
    public struct PotionDefinition : IHaveId
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private Effect _effect;
        [SerializeField] private float _value;
        [SerializeField] private float _time;

        public string Id => _id;
        public Effect Effect => _effect;
        public float Value => _value;
        public float Time => _time;
    }

    public enum Effect
    {
        AddHp,
        SpeedUp
    }
}
