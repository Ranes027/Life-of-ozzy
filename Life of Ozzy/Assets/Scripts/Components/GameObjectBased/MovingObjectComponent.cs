using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Components
{
    public class MovingObjectComponent : MonoBehaviour
    {        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            other.transform.SetParent(transform);
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            other.transform.SetParent(null);
        }
    }
}
