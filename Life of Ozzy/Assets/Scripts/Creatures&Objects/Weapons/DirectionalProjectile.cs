using UnityEngine;

namespace LifeOfOzzy
{
    public class DirectionalProjectile : BaseProjectile
    {
        public void Launch(Vector2 direction)
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.AddForce(direction * _throwSpeed, ForceMode2D.Impulse);

        }
    }

}
