using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Utils;
using LifeOfOzzy.Components;

namespace LifeOfOzzy
{
    public class ShootingTrapWithMeleeAI : ShootingTrapAI
    {
        [Header("Melee Attack Settings")]
        [SerializeField] private Cooldown _meleeAttackCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;

        private static readonly int MeleeKey = Animator.StringToHash("melee");

        protected override void Update()
        {
            base.Update();

            if (_meleeCanAttack.IsTouchingLayer && _meleeAttackCooldown.IsReady)
            {
                MeleeAttack();
            }
        }

        private void MeleeAttack()
        {
            _meleeAttackCooldown.Reset();
            _animator.SetTrigger(MeleeKey);
        }

        public void OnMeleeAttack()
        {            
            _meleeAttack.Check();
        }
    }
}
