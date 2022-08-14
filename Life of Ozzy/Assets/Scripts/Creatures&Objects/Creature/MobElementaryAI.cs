using System.Collections;
using UnityEngine;
using LifeOfOzzy.Components;

namespace LifeOfOzzy
{
    public class MobElementaryAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;
        [SerializeField] private ColliderCheck _attack;

        [SerializeField] private float _alarmTimeDelay = 0.75f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _failAlarmTimeDelay = 0.5f;

        [SerializeField] private float _horizontalTreshold = 0.2f;

        private Coroutine _current;
        private GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private Patrol _patrol;

        private bool _isDead;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            _target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmTimeDelay);
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_attack.IsTouchingLayer)
                {
                    StartState(AttackTarget());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= _horizontalTreshold)
                        _creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }

                yield return null;
            }


            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("Fail");
            yield return new WaitForSeconds(_failAlarmTimeDelay);

            StartState(_patrol.DoPatrol());
        }

        private IEnumerator AttackTarget()
        {
            while (_attack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null) StopCoroutine(_current);

            _current = StartCoroutine(coroutine);

        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);
            _creature.SetDirection(Vector2.zero);

            if (_current != null) StopCoroutine(_current);
        }
    }
}

