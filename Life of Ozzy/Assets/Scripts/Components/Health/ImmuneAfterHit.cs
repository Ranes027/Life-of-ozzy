using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Components
{
    [RequireComponent(typeof(HealthComponent))]
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField] private float _immuneTime;

        private HealthComponent _health;
        private Coroutine _coroutine;

        private CompositeDisposable _trash = new CompositeDisposable();

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _trash.Retain(_health._onDamage.Subscribe(OnDamage));
        }

        private void OnDamage()
        {
            TryStop();
            if (_immuneTime > 0)
                _coroutine = StartCoroutine(ImmuneOn());
        }

        private void TryStop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator ImmuneOn()
        {
            _health.Immune.Retain(this);
            yield return new WaitForSeconds(_immuneTime);
            _health.Immune.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            _trash.Dispose();
        }
    }

}
