using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LifeOfOzzy.Model;

namespace LifeOfOzzy.UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _transitionTime;
        [Header("Level Title")]
        [SerializeField] private TextMeshProUGUI _levelNumber;
        [SerializeField] private TextMeshProUGUI _levelTitle;

        private static readonly int Enabled = Animator.StringToHash("Enabled");
        private static readonly int TitleKey = Animator.StringToHash("Title");

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnAfterSceneLoad()
        {
            InitLoader();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private static void InitLoader()
        {
            SceneManager.LoadScene("LevelLoader", LoadSceneMode.Additive);
        }

        public void LoadLevel(string sceneName)
        {
            StartCoroutine(StartAnimation(sceneName));
        }

        private IEnumerator StartAnimation(string sceneName)
        {
            _animator.SetBool(Enabled, true);
            yield return new WaitForSeconds(_transitionTime);            
            SceneManager.LoadScene(sceneName);
            _animator.SetBool(Enabled, false);
        }

        public void SetLevelTitle(string number, string title)
        {
            _levelNumber.text = number.ToString();
            _levelTitle.text = LocalizationManager.I.Localize(title.ToString());

            _animator.SetTrigger(TitleKey);
        }
    }

}

