using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;
using TMPro;

namespace LifeOfOzzy.UI
{
    public class HudController : MonoBehaviour
    {
        [Header("Bars Settings")]
        [SerializeField] private ProgressBarWidget _healthBar;
        [Header("Panels Settings")]
        [SerializeField] private TextMeshProUGUI _moneyPanel;
        [SerializeField] private GameObject _expPanel;
        [SerializeField] private TextMeshProUGUI _expValue;
        [SerializeField] private TextMeshProUGUI _healthPotionPanel;
        [Header("Menu Icons Settings")]
        [SerializeField] private GameObject _statsManagerMenu;
        [Header("Items Icons Setting")]
        [SerializeField] private GameObject _flashlight;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private const string CoinId = "Coin";
        private const string ExpId = "Exp";
        private const string PotionId = "HealthPotion";
        private const string FlashlightId = "Flashlight";

        private int CoinCount => _session.Data.Inventory.Count(CoinId);
        private int PotionsCount => _session.Data.Inventory.Count(PotionId);
        private int ExpCount => _session.Data.Inventory.Count(ExpId);
        private int FlashlightCount => _session.Data.Inventory.Count(FlashlightId);

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _moneyPanel.text = CoinCount.ToString();
            _expValue.text = ExpCount.ToString();
            _healthPotionPanel.text = PotionsCount.ToString();
            if (FlashlightCount > 0) _flashlight.SetActive(true);

            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));

            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = (int)_session.StatsModel.GetValue(StatId.Hp);
            var value = (float)newValue / maxHealth;

            _healthBar.SetProgress(value);
        }

        public void OnInventoryChanged(string id, int value)
        {
            if (id == CoinId)
                _moneyPanel.text = CoinCount.ToString();

            if (id == ExpId)
                _expValue.text = ExpCount.ToString();

            if (id == PotionId)
                _healthPotionPanel.text = PotionsCount.ToString();

            if (id == FlashlightId)
            {
                _flashlight.SetActive(true);
                _statsManagerMenu.SetActive(true);
                _expPanel.SetActive(true);
            }

        }

        public void OnSettings()
        {
            WindowUtils.CreateWindow("UI/InGameMenu");
        }

        public void OnStatsManager()
        {
            WindowUtils.CreateWindow("UI/StatsWindow");
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
            _trash.Dispose();
        }
    }

}
