using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _throwSpeed;
        [SerializeField] protected bool _invertX;

        protected Rigidbody2D Rigidbody;
        protected int Direction;

        protected virtual void Start()
        {
            var modify = _invertX ? -1 : 1;
            Direction = modify * transform.lossyScale.x > 0 ? 1 : -1;
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }

}
