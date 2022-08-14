using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy
{
    public class NoPatrol : Patrol
    {
        public override IEnumerator DoPatrol()
        {
            yield return null;
        }
    }

}
