using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodController : MonoBehaviour
{
    [SerializeField] private Animator _floodAnimator;
    [SerializeField] private float _floodTime;
    [SerializeField] private bool _permanent = false;

    private Coroutine _coroutine;

    public void StartFlooding()
    {
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        _floodAnimator.SetBool("is-flooding", true);
        yield return new WaitForSeconds(_floodTime);
        if (_permanent == false)
            _floodAnimator.SetBool("is-flooding", false);

        _coroutine = null;
    }
}
