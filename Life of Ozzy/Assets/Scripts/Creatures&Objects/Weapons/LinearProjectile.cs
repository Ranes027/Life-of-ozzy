using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public class LinearProjectile : BaseProjectile
    {
        protected override void Start()
        {
            base.Start();
        }
        
        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += Direction * _throwSpeed;
            Rigidbody.MovePosition(position);
        }
    }
}

