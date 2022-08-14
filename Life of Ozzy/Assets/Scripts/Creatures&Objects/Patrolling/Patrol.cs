using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }

}
