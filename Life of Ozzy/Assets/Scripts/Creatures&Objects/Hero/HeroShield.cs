using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Components;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy
{
    public class HeroShield : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Cooldown _cooldown;

        public void Use()
        {
            _health.Immune.Retain(this);
            _cooldown.Reset();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if(_cooldown.IsReady)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _health.Immune.Release(this);
        }
    }

}
