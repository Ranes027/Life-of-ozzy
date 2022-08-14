using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LifeOfOzzy.UI
{
    public class CustomButton : Button
    {
        [SerializeField] private GameObject _normal;
        [SerializeField] private GameObject _highlighted;
        [SerializeField] private GameObject _pressed;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            var isPressed = state == SelectionState.Pressed || state == SelectionState.Disabled;
            _normal.SetActive(state != SelectionState.Pressed);
            _highlighted.SetActive(state == SelectionState.Highlighted);
            _pressed.SetActive(state == SelectionState.Pressed);
                
        }
    }
}

