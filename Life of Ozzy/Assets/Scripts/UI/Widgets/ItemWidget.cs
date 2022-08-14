using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LifeOfOzzy.Model;

namespace LifeOfOzzy.UI
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _value;

        public void SetData(ItemWithCount price)
        {
            var def = DefinitionsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }

}
