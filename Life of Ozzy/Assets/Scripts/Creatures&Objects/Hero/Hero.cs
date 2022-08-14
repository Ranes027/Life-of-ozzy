using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Animations;
using LifeOfOzzy.Components;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy
{
    public class Hero : Creature, IAddInInventory
    {
        [Header("Additional Parameters")]
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private float _onLadderMovementSpeed;
        [SerializeField] private float _landVelocity;
        [SerializeField] private float _defaultJumpPower;
        [SerializeField] private float _buffJumpPower;

        [Header("Additional Checkers")]
        [SerializeField] private ColliderCheck _ladderCheck;
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private GameObject _flashlight;

        [Header("Animator Controller Settings")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Header("Particle Systems")]
        [SerializeField] private ParticleSystem _healParticles;
        [SerializeField] private ParticleSystem _lostCoinParticles;
        [SerializeField] private SpawnComponent _throwSpawner;

        private bool _onLadder;
        private bool _allowDoubleJump;
        private Collider2D[] _interactionResult = new Collider2D[1];

        private GameSession _session;
        private HealthComponent _health;
        private float _defaultGravityScale;
        private float _additionalSpeed;
        private int _damageBuff;

        private CameraShakeEffect _cameraShake;
        private static readonly int OnLadderKey = Animator.StringToHash("on-ladder");
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private const string CoinId = "Coin";
        private const string SwordId = "Sword";
        private const string PotionId = "HealthPotion";
        private const string FlashlightId = "Flashlight";
        private int CoinCount => _session.Data.Inventory.Count(CoinId);
        private int SwordCount => _session.Data.Inventory.Count(SwordId);
        private int PotionsCount => _session.Data.Inventory.Count(PotionId);
        private int FlashlightCount => _session.Data.Inventory.Count(FlashlightId);

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private readonly Cooldown _speedUpCooldown = new Cooldown();

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();
            _cameraShake = FindObjectOfType<CameraShakeEffect>();

            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgraded += OnHeroUpgraded;

            _health.SetHealth(_session.Data.Hp.Value);

            UpdateHeroWeapon();
        }

        private void OnHeroUpgraded(StatId statId)
        {
            switch (statId)
            {
                case StatId.Hp:
                    var health = (int)_session.StatsModel.GetValue(statId);
                    _session.Data.Hp.Value = health;
                    _health.SetHealth(health);
                    break;
            }

        }

        public void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId) UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }

        protected override void Update()
        {
            base.Update();
            _onLadder = OnLadder();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Animator.SetBool(OnLadderKey, _onLadder);
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _allowDoubleJump = true;
            }

            if (_onLadder)
            {
                Rigidbody.bodyType = RigidbodyType2D.Kinematic;
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Direction.y * _onLadderMovementSpeed);
            }
            else
            {
                Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (_session.PerksModel.IsJumpBuffSupported)
                _jumpPower = _buffJumpPower;
            else
                _jumpPower = _defaultJumpPower;

            if (!IsGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                return _jumpPower;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        private bool OnLadder()
        {
            return _ladderCheck.IsTouchingLayer;
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            _cameraShake.Shake();

            if (CoinCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinCount, 3);
            _session.Data.Inventory.Remove(CoinId, numCoinsToDispose);

            var burst = _lostCoinParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _lostCoinParticles.emission.SetBurst(0, burst);

            _lostCoinParticles.gameObject.SetActive(true);
            _lostCoinParticles.Play();
            Debug.Log($"You lost {numCoinsToDispose}. Total coins: {CoinCount}");
        }

        public void SpawnHealParticles()
        {
            _healParticles.gameObject.SetActive(true);
            _healParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _landVelocity)
                {
                    _particles.Spawn("Land");
                }
            }
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;
            base.Attack();
        }

        public override void OnDoAttack()
        {
            base.OnDoAttack();
            MeleeDamageStat(_attackRange);
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        public void OnDoThrow()
        {
            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDef = DefinitionsFacade.I.ThrowableItems.Get(throwableId);

            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();

            _session.Data.Inventory.Remove(throwableId, 1);
        }

        public void Throw()
        {
            if (!_throwCooldown.IsReady || !CanThrow) return;

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();

        }

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId) return SwordCount > 1;

                var def = DefinitionsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        public void UsePotion()
        {
            var potion = DefinitionsFacade.I.Potions.Get(PotionId);

            if (PotionsCount > 0)
            {
                switch (potion.Effect)
                {
                    case Effect.AddHp:
                        if (_session.PerksModel.IsHealBuffSupported) _session.Data.Hp.Value += (int)potion.Value; ;
                        _session.Data.Hp.Value += (int)potion.Value;
                        var health = (int)_session.StatsModel.GetValue(StatId.Hp);
                        if (_session.Data.Hp.Value > health) break;
                        _health.SetHealth(_session.Data.Hp.Value);
                        Sounds.Play("Heal");
                        SpawnHealParticles();
                        break;
                    case Effect.SpeedUp:
                        _speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
                        _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                        _speedUpCooldown.Reset();
                        break;
                }

                _session.Data.Inventory.Remove(potion.Id, 1);
            }
        }

        protected override float CalculateSpeed()
        {
            if (_speedUpCooldown.IsReady)
                _additionalSpeed = 0f;

            var defaultSpeed = _session.StatsModel.GetValue(StatId.Speed);
            return defaultSpeed + _additionalSpeed;
        }

        private void MeleeDamageStat(CheckCircleOverlap AttackRange)
        {
            var hpModify = AttackRange.GetComponent<HealOrDamageComponent>();
            var damageValue = (int)_session.StatsModel.GetValue(StatId.Damage);
            if (_session.PerksModel.IsDamageBuffSupported)
                _damageBuff = 2;
            else
                _damageBuff = 1;
            hpModify.SetDelta(-damageValue * _damageBuff);
        }

        public void UseFlashlight()
        {
            if (FlashlightCount > 0)
            {
                var isActive = _flashlight.gameObject.activeSelf;
                _flashlight.gameObject.SetActive(!isActive);                 
            }
        }
    }
}
