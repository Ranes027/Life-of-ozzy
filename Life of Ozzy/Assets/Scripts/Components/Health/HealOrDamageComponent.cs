using UnityEngine;

namespace LifeOfOzzy.Components
{
    public class HealOrDamageComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void SetDelta(int delta)
        {
            _hpDelta = delta;
        }

        public void ModifyHealth (GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            healthComponent?.ChangeHealthPoints(_hpDelta);
        }
    }

}
