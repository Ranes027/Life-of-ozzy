using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverLap;
        private readonly Collider2D[] _interactionResult = new Collider2D[10];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _interactionResult,
                _layer);

            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var isInTags = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (isInTags)
                {
                    _onOverLap?.Invoke(overlapResult.gameObject);
                }
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
        }
    }
}

