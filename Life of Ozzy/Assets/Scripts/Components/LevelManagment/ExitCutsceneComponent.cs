using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Model;
using LifeOfOzzy.UI;

namespace LifeOfOzzy.Components
{
    public class ExitCutsceneComponent : ExitLevelComponent
    {
        public override void Exit()
        {
            var loader = FindObjectOfType<LevelLoader>();
            if (_allowLevelName)
                loader.SetLevelTitle(_number, _title);

            loader.LoadLevel(_sceneName);
        }
    }

}
