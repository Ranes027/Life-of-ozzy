using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;


namespace LifeOfOzzy.UI
{
    public class ShopWindow : AnimatedWindow
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private ShopWidget _prefab;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _info;
        [SerializeField] private ItemWidget _price;
        [SerializeField] private Button _buyButton;

        [Header("DataSet Settings")]
        [SerializeField] private int _minItemsCount;
        [SerializeField] private int _startIndex;

        private LimitedDataGroup<ShopDefinition, ShopWidget> _dataGroup;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        protected override void Start()
        {
            base.Start();

            _dataGroup = new LimitedDataGroup<ShopDefinition, ShopWidget>(_prefab, _itemsContainer);

            _session = FindObjectOfType<GameSession>();

            _trash.Retain(_session.ShopModel.Subscribe(OnShopChanged));
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));

            OnShopChanged();
        }

        private void OnShopChanged()
        {
            _dataGroup.SetLimitedData(DefinitionsFacade.I.Shop.All, _minItemsCount, _startIndex);

            var selected = _session.ShopModel.InterfaceSelectedItem.Value;

            _buyButton.interactable = _session.ShopModel.CanBuy(selected);

            var def = DefinitionsFacade.I.Shop.Get(selected);
            _price.SetData(def.Price);

            _info.text = LocalizationManager.I.Localize(def.Info);
            _name.text = LocalizationManager.I.Localize(def.Name);
        }

        private void OnBuy()
        {
            var selected = _session.ShopModel.InterfaceSelectedItem.Value;
            _session.ShopModel.Buy(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

}
