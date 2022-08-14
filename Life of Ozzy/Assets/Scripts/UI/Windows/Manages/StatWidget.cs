using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LifeOfOzzy.Utils;
using LifeOfOzzy.Model;
using LifeOfOzzy.Components;

namespace LifeOfOzzy.UI
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDefinition>
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _currentValue;
        [SerializeField] private TextMeshProUGUI _increaseValue;
        [SerializeField] private ProgressBarWidget _progress;
        [SerializeField] private GameObject _selector;

        private GameSession _session;
        private StatDefinition _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }


        public void SetData(StatDefinition data, int index)
        {
            _data = data;
            if (_session != null) UpdateView();
        }

        private void UpdateView()
        {
            var statsModel = _session.StatsModel;

            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.I.Localize(_data.Name);
            var currentLevelValue = statsModel.GetValue(_data.Id);
            _currentValue.text = currentLevelValue.ToString();

            var currentLevel = statsModel.GetCurentLevel(_data.Id);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = statsModel.GetValue(_data.Id, nextLevel);
            var increaseValue = nextLevelValue - currentLevelValue;
            _increaseValue.text = $"+{increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevel = DefinitionsFacade.I.Player.GetStat(_data.Id).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float)maxLevel);

            _selector.SetActive(statsModel.InterfaceSelectedStat.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
        }
    }

}
