using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeOfOzzy.Model;
using TMPro;

namespace LifeOfOzzy.UI
{
    public class ShopWidget : MonoBehaviour, IItemRenderer<ShopDefinition>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isSelected;
        [SerializeField] private GameObject _isOutOfStock;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private int _value = 0;

        private GameSession _session;
        private ShopDefinition _shopData;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _value = DefinitionsFacade.I.Shop.Get(_shopData.Id).Amount;
            UpdateView();
        }

        public void SetData(ShopDefinition data, int index)
        {
            _shopData = data;
            if (_session != null) UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _shopData.Icon;
            _isSelected.SetActive(_session.ShopModel.InterfaceSelectedItem.Value == _shopData.Id);
            _isOutOfStock.SetActive(DefinitionsFacade.I.Shop.Get(_shopData.Id).Amount <= 0);
            _amount.text = _value.ToString();
            
        }

        public void OnSelect()
        {
            _session.ShopModel.InterfaceSelectedItem.Value = _shopData.Id;
        }
    }

}
