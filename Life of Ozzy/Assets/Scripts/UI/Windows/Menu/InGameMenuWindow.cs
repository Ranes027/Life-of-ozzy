using UnityEngine;
using UnityEngine.SceneManagement;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.UI
{
    public class InGameMenuWindow : AnimatedWindow
    {
        private float _defaultTimeScale;

        protected override void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnExit()
        {
            SceneManager.LoadScene("StartScreen");

            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
            AudioListener.pause = false;
        }
    }
}

