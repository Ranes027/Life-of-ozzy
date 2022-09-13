using System;
using UnityEngine;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.UI
{
    public class MainMenuWindow : AnimatedWindow
    {
        [SerializeField] private string _startLevelName;

        private Action _closeAction;

        public void OnStartGame()
        {
            var loader = FindObjectOfType<LevelLoader>();
            loader.LoadLevel(_startLevelName);
            Close();
        }

        public void OnShowSettings()
        {            
            WindowUtils.CreateWindow("UI/SettingsWindow");       
        }

        public void OnExitGame()
        {

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }

}
