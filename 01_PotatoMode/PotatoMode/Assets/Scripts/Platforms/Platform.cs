using PotatoMode.Platforms.Data;
using UnityEngine;


namespace PotatoMode.Platforms
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Platform : MonoBehaviour
    {
        private Collider2D _collider;
        
        
        private void OnTriggerExit2D(Collider2D intruder)
        {
            if(intruder.gameObject.TryGetComponent<IConsumer>(out var consumer))
            {
                OnExit(consumer);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D intruder)
        {
            if(intruder.gameObject.TryGetComponent<IConsumer>(out var consumer))
            {
                OnEnter(consumer);
            }
        }
        

        protected abstract void OnEnter(IConsumer consumer);
        protected abstract void OnExit(IConsumer consumer);
    }
}