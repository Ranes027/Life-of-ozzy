using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.UI;

namespace LifeOfOzzy.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            var loader = FindObjectOfType<LevelLoader>();
            loader.LoadLevel(_sceneName);
        }
    }
}

