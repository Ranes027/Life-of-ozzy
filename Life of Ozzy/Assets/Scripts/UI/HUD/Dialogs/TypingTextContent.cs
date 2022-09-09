using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;
using LifeOfOzzy.Components;

namespace LifeOfOzzy.UI
{
    public class TypingTextContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _textSpeed = 0.09f;

        [Space][SerializeField] private Button _continueButton;
        [SerializeField] private UnityEvent _onComplete;

        [Header("Sounds")]
        [SerializeField] private AudioClip _typing;

        private Coroutine _typingRoutine;

        private AudioSource _sfxSource;
        private string _sentence;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();

            _text.alpha = 0;
        }

        [ContextMenu("Show")]
        public void ShowText()
        {
            StartTypeAnimation();
        }

        private IEnumerator TypeText()
        {
            _sentence = _text.text.ToString();
            _text.text = string.Empty;
            _text.alpha = 1;

            foreach (var letter in _sentence)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        public void OnContinue()
        {
            StopTypeAnimation();

            var isTextComplete = _sentence.Length >= _text.text.Length;
            if(isTextComplete)
                _onComplete?.Invoke();
            else
                StartTypeAnimation();
        }

        private void StartTypeAnimation()
        {
            _typingRoutine = StartCoroutine(TypeText());
        }

        private void StopTypeAnimation()
        {
            if(_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            _typingRoutine = null;
        }

    }

}
