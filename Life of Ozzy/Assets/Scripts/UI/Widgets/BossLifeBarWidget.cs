using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Components;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.UI
{
    public class BossLifeBarWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private CanvasGroup _canvas;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private float _maxHealth;

        private void Start()
        {
            _maxHealth = _health.Health;
            _trash.Retain(_health._onChange.Subscribe(OnHpChanged));
            _trash.Retain(_health._onDie.Subscribe(HideUI));
        }

        private void OnHpChanged(int hp)
        {
            _hpBar.SetProgress(hp / _maxHealth);
        }

        [ContextMenu("Hide")]
        private void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        [ContextMenu("Show")]
        private void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _canvas.alpha = alpha;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

}
