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

        private GameSession _session;
        private ShopDefinition _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(ShopDefinition data, int index)
        {
            _data = data;
            if (_session != null) UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isSelected.SetActive(_session.ShopModel.InterfaceSelectedItem.Value == _data.Id);
            _isOutOfStock.SetActive(DefinitionsFacade.I.Shop.Get(_data.Id).Amount > 0);
            
        }

        public void OnSelect()
        {
            _session.ShopModel.InterfaceSelectedItem.Value = _data.Id;
        }
    }

}
