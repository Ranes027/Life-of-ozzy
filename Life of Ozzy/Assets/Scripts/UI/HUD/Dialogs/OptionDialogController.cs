using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using LifeOfOzzy.Model;
using LifeOfOzzy.UI;

namespace LifeOfOzzy.UI
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private TextMeshProUGUI _contentText;
        [SerializeField] private Transform _optionContainer;
        [SerializeField] private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _optionContainer);
        }

        public void OnOptionSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);
            _contentText.text = LocalizationManager.I.Localize(data.DialogText);

            _dataGroup.SetData(data.Options);
        }
    }

    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }

    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}
