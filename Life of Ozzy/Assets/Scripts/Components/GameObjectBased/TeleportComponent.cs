using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destinationTransform;

        public void Teleport (GameObject target)
        {
            target.transform.position = _destinationTransform.position;
        }
    }
}
