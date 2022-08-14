using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LifeOfOzzy.UI
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }

}
