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
            if (_verticalDirection)
                position.y += Direction * _throwSpeed;
            else
                position.x += Direction * _throwSpeed;

            Rigidbody.MovePosition(position);
        }
    }
}

