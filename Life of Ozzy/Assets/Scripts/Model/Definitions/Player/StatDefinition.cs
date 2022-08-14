using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace LifeOfOzzy.Model
{
    [Serializable]
    public struct StatDefinition
    {
        [SerializeField] private StatId _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevelDef[] _levels;

        public StatId Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public StatLevelDef[] Levels => _levels;
    }

    [Serializable]
    public struct StatLevelDef
    {
        [SerializeField] private float _value;
        [SerializeField] private ItemWithCount _price;

        public float Value => _value;
        public ItemWithCount Price => _price;
    }

    public enum StatId
    {
        Hp,
        Damage,
        Speed
    }

}
