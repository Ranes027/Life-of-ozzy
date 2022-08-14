using UnityEngine;
using UnityEngine.Events;

namespace LifeOfOzzy.Components
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        [SerializeField] private GameObject _interactParticle;
        [SerializeField] private string _tag = "Player";

        public void Interact()
        {
            _action?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag)) _interactParticle.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag)) _interactParticle.SetActive(false);
        }

        private void OnDisable()
        {
            Destroy(this);
            Destroy(_interactParticle, 3f);
        }
    }

}
