using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShootController : MonoBehaviour
{
    [SerializeField] private Animator[] _shootAnimator;
    [SerializeField] private string _trigger;

    public void StartShooting()
    {
        for (int i = 0; i < _shootAnimator.Length; i++)
        {
            _shootAnimator[i].SetTrigger(_trigger);
        }
    }
}
