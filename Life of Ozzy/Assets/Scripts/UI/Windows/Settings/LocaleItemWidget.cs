using System;
using UnityEngine;
using UnityEngine.Events;
using LifeOfOzzy.Model;
using TMPro;

namespace LifeOfOzzy.UI
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.I.OnLocaleChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _data = localeInfo;
            UpdateSelection();
            _text.text = localeInfo.LocaleId.ToUpper();
        }

        private void UpdateSelection()
        {
            var isSelected = LocalizationManager.I.LocaleKey == _data.LocaleId;
            _selector.SetActive(isSelected);
        }


        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= UpdateSelection;
        }
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {

    }

    public class LocaleInfo
    {
        public string LocaleId;
    }

}
