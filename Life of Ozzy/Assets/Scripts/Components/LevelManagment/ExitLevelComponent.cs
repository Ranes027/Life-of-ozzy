using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.UI;

namespace LifeOfOzzy.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] protected string _sceneName;
        [Header("Level Name")]
        [SerializeField] protected bool _allowLevelName;
        [SerializeField] protected string _number;
        [SerializeField] protected string _title;

        public bool AllowLevelName => _allowLevelName;

        public virtual void Exit()
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

