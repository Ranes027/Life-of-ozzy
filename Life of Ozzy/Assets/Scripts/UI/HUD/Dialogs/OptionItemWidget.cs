using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using LifeOfOzzy.Model;

namespace LifeOfOzzy.UI
{
    public class OptionItemWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private SelectOption _onSelect;

        private OptionData _data;

        public void SetData(OptionData data, int index)
        {
            _data = data;                  
            _label.text = LocalizationManager.I.Localize(data.Text);
        }

        public void OnSelect()
        {
            _onSelect.Invoke(_data);
        }

        [Serializable]
        public class SelectOption : UnityEvent<OptionData>
        {
        }
    }

}
