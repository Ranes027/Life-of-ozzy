using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Utils;
using LifeOfOzzy.Components;

namespace LifeOfOzzy
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] protected ColliderCheck _vision;        

        [Header("Range Attack Settings")]
        [SerializeField] protected Cooldown _rangeAttackCooldown;
        [SerializeField] protected SpawnComponent _rangeAttack;

        protected Animator _animator;        
        protected static readonly int RangeKey = Animator.StringToHash("range");
        protected static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            if (_vision.IsTouchingLayer && _rangeAttackCooldown.IsReady)
            {
                RangeAttack();
            }            
        }

        protected virtual void RangeAttack()
        {
            _rangeAttackCooldown.Reset();
            _animator.SetTrigger(RangeKey);
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }                

        public void OnDie()
        {
            _animator.SetBool(IsDeadKey, true);
        }
    }
}

