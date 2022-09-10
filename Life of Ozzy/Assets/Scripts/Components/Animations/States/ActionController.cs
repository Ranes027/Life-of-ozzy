using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionController : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnterAnimation;

    public void Action()
    {
        _onEnterAnimation?.Invoke();
    }
}
