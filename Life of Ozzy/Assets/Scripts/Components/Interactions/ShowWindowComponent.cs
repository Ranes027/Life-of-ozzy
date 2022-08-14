using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Components
{
    public class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField] private string _windowPath;

        public void Show()
        {
            WindowUtils.CreateWindow(_windowPath);
        }
    }

}
