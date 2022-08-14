using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LifeOfOzzy.Components
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private TimerData[] _timers;

        public void Set(int index)
        {
            var timer = _timers[index];
            if (timer.Coroutine != null) StopCoroutine(timer.Coroutine);

            timer.Coroutine = StartCoroutine(StartTimer(timer));
        }

        private IEnumerator StartTimer(TimerData timerData)
        {
            yield return new WaitForSeconds(timerData.Delay);
            timerData.TimesUp?.Invoke();
        }
    }

    [Serializable]
    public class TimerData
    {
        public float Delay;
        public UnityEvent TimesUp;
        public Coroutine Coroutine;
    }
}
