using System;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private Sentence[] _sentences;
        [SerializeField] DialogType _type;

        public Sentence[] Sentences => _sentences;
        public DialogType Type => _type;
    }

    [Serializable]
    public struct Sentence
    {
        [SerializeField] private string _valued;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Side _side;

        public string Value => _valued;
        public string Name => _name;
        public Sprite Icon => _icon;
        public Side Side => _side;
    }

    public enum Side
    {
        Left,
        Right
    }

    public enum DialogType
    {
        Simple,
        Personalized
    }

}
