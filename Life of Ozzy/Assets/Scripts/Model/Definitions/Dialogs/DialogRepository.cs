using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.UI;
using LifeOfOzzy.Components;

namespace LifeOfOzzy.Model
{
    [CreateAssetMenu(menuName = "Definitions/Dialogs", fileName = "Dialogs")]
    public class DialogRepository : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;


    }

}
