using UnityEngine;
using LifeOfOzzy.Components;
using LifeOfOzzy.Utils;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private HealthComponent _hp;
    [SerializeField] private Animator _animator;

    private static readonly int Health = Animator.StringToHash("health");

    private readonly CompositeDisposable _trash = new CompositeDisposable();

    private void Awake()
    {
        _hp._onChange.Subscribe(OnHealthChange);
        OnHealthChange(_hp.Health);
    }

    private void OnHealthChange(int health)
    {
        _animator.SetInteger(Health, health);
    }

    private void OnDestroy()
    {
        _trash.Dispose();
    }
}
