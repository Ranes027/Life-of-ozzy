using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Components
{
    public class PlaySoundOnTrigger : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField][Range(0, 1)] private float _volumeLimit = 1f;
        [SerializeField] private string _tag;

        [Header("Fade Track Options")]
        [SerializeField] private float _timeToFade = 1f;
        private float _timeElapsed = 0f;

        private Coroutine _current;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag) && !_source.isPlaying)
            {
                if (_current != null) StopCoroutine(_current);

                _current = StartCoroutine(FadeTrackIn(_source, _volumeLimit));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            if (other.gameObject.CompareTag(_tag) && _source.isPlaying)
            {
                if (_current != null) StopCoroutine(_current);

                _current = StartCoroutine(FadeTrackOut(_source, _volumeLimit));
            }

        }

        private IEnumerator FadeTrackOut(AudioSource source, float volumeLimit)
        {

            _timeElapsed = 0f;
            while (_timeElapsed < _timeToFade)
            {
                source.volume = Mathf.Lerp(volumeLimit, 0, _timeElapsed / _timeToFade);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            source.Stop();
        }

        private IEnumerator FadeTrackIn(AudioSource source, float volumeLimit)
        {
            source.Play();
            _timeElapsed = 0f;
            while (_timeElapsed < _timeToFade)
            {
                source.volume = Mathf.Lerp(0, volumeLimit, _timeElapsed / _timeToFade);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

    }
}


