using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/Localization", fileName = "Localization")]
    public class LocalizationRepository : ScriptableObject
    {
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _localeItems;

        private UnityWebRequest _request;

        public Dictionary<string, string> GetData()
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var localeItem in _localeItems)
            {
                dictionary.Add(localeItem.Key, localeItem.Value);
            }

            return dictionary;
        }

        [ContextMenu("Update Locale")]
        public void LoadLocale()
        {
            if (_request != null) return;

            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnDataLoaded;
        }

        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var data = _request.downloadHandler.text;
                ParseData(data);
            }
        }

        private void ParseData(string data)
        {
            var rows = data.Split('\n');
            _localeItems.Clear();
            foreach(var row in rows)
            {
                AddLocaleItem(row);
            }
        }

        private void AddLocaleItem(string row)
        {
            try
            {
                var parts = row.Split('\t');
                _localeItems.Add(new LocaleItem { Key = parts[0], Value = parts[1] });
            }
            catch (Exception e)
            {
                Debug.LogError($"Cant parse row:{row}.\n{e}");
            }
        }

        [Serializable]
        private class LocaleItem
        {
            public string Key;
            public string Value;
        }
    }

}
