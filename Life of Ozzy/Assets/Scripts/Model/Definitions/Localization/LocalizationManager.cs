using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LifeOfOzzy.Model
{
    public class LocalizationManager
    {
        public readonly static LocalizationManager I;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localization/current");
        private Dictionary<string, string> _localization;

        public event Action OnLocaleChanged;
        public string LocaleKey => _localeKey.Value;

        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }        

        private void LoadLocale(string localeToLoad)
        {
            var def = Resources.Load<LocalizationRepository>($"Locales/{localeToLoad}");
            _localization = def.GetData();
            _localeKey.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"###{key}###";
        }

        public void SetLocale(string localeKey)
        {
            LoadLocale(localeKey);
        }
    }

}
