using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _requiredLayer;
        [SerializeField] protected bool _isTouchingLayer;

        public bool IsTouchingLayer => _isTouchingLayer;
        
    }
}

