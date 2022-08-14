using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LifeOfOzzy.Components
{
    public class CheatController : MonoBehaviour
    {
        [SerializeField] private float _inputTimeToLive;
        [SerializeField] private CheatItem[] _cheats;

        private string _currentInput;
        private float _inputTime;

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput;
        }

        private void OnDestroy()
        {
            Keyboard.current.onTextInput -= OnTextInput;
        }

        private void OnTextInput(char inputChar)
        {
            _currentInput += inputChar;
            _inputTime = _inputTimeToLive;
            FindAnyCheats();
        }

        private void FindAnyCheats()
        {
            foreach (var CheatItem in _cheats)
            {
                if (_currentInput.Contains(CheatItem.CheatName))
                {
                    CheatItem.Action.Invoke();
                    _currentInput = string.Empty;
                }
            }
        }

        private void Update()
        {
            if (_inputTime < 0)
            {
                _currentInput = string.Empty;
            }
            else
            {
                _inputTime -= Time.deltaTime;
            }
        }
        
        [Serializable]
        public class CheatItem
        {
            public string CheatName;
            public UnityEvent Action;
        }
    }
}



