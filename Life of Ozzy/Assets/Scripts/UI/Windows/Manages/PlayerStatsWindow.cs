using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.UI
{
    public class PlayerStatsWindow : AnimatedWindow
    {
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private StatWidget _prefab;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private ItemWidget _price;

        private DataGroup<StatDefinition, StatWidget> _dataGroup;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<StatDefinition, StatWidget>(_prefab, _statsContainer);

            _session = FindObjectOfType<GameSession>();
            _session.StatsModel.InterfaceSelectedStat.Value = DefinitionsFacade.I.Player.Stats[0].Id;

            _trash.Retain(_session.StatsModel.Subscribe(OnStatChanged));
            _trash.Retain(_upgradeButton.onClick.Subscribe(OnUpgrade));

            OnStatChanged();
        }        

        private void OnStatChanged()
        {
            var stats = DefinitionsFacade.I.Player.Stats;
            _dataGroup.SetData(stats);

            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            var nextLevel = _session.StatsModel.GetCurentLevel(selected) + 1;
            var def = _session.StatsModel.GetLevelDef(selected, nextLevel);
            _price.SetData(def.Price);

            _price.gameObject.SetActive(def.Price.Count != 0);
            _upgradeButton.gameObject.SetActive(def.Price.Count != 0);
        }

        private void OnUpgrade()
        {
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            _session.StatsModel.LevelUp(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

}
