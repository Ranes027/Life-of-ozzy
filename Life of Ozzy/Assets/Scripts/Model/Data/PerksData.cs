using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringObservableProperty _used = new StringObservableProperty();
        [SerializeField] private List<string> _unlocked;

        public StringObservableProperty Used => _used;

        public void AddPerk(string id)
        {
            if (!_unlocked.Contains(id))
                _unlocked.Add(id);
        }

        public bool IsUnlocked(string id)
        {
            return _unlocked.Contains(id);
        }
    }

}
