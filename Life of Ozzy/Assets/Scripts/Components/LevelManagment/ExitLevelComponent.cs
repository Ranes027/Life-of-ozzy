using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.UI;

namespace LifeOfOzzy.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [Header("Level Name")]
        [SerializeField] private bool _allowLevelName;
        [SerializeField] private string _number;
        [SerializeField] private string _title;

        public bool AllowLevelName => _allowLevelName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            var loader = FindObjectOfType<LevelLoader>();
            if (_allowLevelName)
                loader.SetLevelTitle(_number, _title);

            loader.LoadLevel(_sceneName);
        }
    }
}

