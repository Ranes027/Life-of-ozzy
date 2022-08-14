using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent<GameObject> _action;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag) || other.gameObject.IsInLayer(_layer))
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }

}
