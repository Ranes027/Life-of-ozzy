using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _throwSpeed;
        [Header("Direction settings")]
        [SerializeField] protected bool _invert;
        [SerializeField] protected bool _verticalDirection;

        protected Rigidbody2D Rigidbody;
        protected int Direction;

        protected virtual void Start()
        {
            var modify = _invert ? -1 : 1;
            if (_verticalDirection)
                Direction = modify * transform.lossyScale.y > 0 ? 1 : -1;
            else
                Direction = modify * transform.lossyScale.x > 0 ? 1 : -1;

            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }

}
