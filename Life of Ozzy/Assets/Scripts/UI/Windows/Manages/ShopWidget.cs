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
        [SerializeField] private GameObject _amountIcon;
        [SerializeField] private TextMeshProUGUI _amount;

        private GameSession _session;
        private ShopDefinition _shopData;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
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
            _isOutOfStock.SetActive(_session.ShopModel.GetCurrentAmount(_shopData.Id) <= 0);
            _amountIcon.SetActive(_session.ShopModel.GetCurrentAmount(_shopData.Id) > 0);
            _amount.text = _session.ShopModel.GetCurrentAmount(_shopData.Id).ToString();
            
        }

        public void OnSelect()
        {
            _session.ShopModel.InterfaceSelectedItem.Value = _shopData.Id;
        }
    }

}
