using System;
using UnityEngine;

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
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);       
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
