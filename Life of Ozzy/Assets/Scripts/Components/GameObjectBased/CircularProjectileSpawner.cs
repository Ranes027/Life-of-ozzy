using System.Collections;
using UnityEngine;
using LifeOfOzzy.Utils;

namespace LifeOfOzzy.Components
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private DirectionalProjectile _projectile;
        [SerializeField] private int _burstCount;        
        [SerializeField] private float _delay;

        [ContextMenu("Launch")]
        public void LaunchProjectiles()
        {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            var sectorStep = 2 * Mathf.PI / _burstCount;
            for (var i = 0; i < _burstCount; i++)
            {
                var angle = sectorStep * i;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                var instance = SpawnUtils.Spawn(_projectile.gameObject, transform.position);
                var projectile = instance.GetComponent<DirectionalProjectile>();
                projectile.Launch(direction);
                yield return new WaitForSeconds(_delay);
            }
        }
    }

}
