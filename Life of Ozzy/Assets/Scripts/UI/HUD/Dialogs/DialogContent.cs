using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LifeOfOzzy.Components;

namespace LifeOfOzzy.UI
{
    public class DialogContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;

        public TextMeshProUGUI Text => _text;
        public TextMeshProUGUI Name => _name;

        public void TrySetIcon(Sprite icon)
        {
            if (_icon != null) _icon.sprite = icon;
        }

        public void TrySetName(string name)
        {
            if (_name != null) _name.text = name;
        }
    }

}
