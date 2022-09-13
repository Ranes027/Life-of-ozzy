using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.UI
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }

        public void OnLanguages()
        {
            WindowUtils.CreateWindow("UI/LanguageWindow");
        }

        public void OnControls()
        {
            WindowUtils.CreateWindow("UI/ControlsWindow");
        }
    }

}
