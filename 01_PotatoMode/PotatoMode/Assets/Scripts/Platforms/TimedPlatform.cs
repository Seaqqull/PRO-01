using PotatoMode.Platforms.Data;
using UnityEngine;


namespace PotatoMode.Platforms
{
    public class TimedPlatform : Platform
    {
        [SerializeField] [Range(0.0f, byte.MaxValue)] private float _destroyTime = 1.0f;
        
        private bool _activated;
        
        
        protected override void OnEnter(IConsumer consumer)
        {
            if (_activated)
                return;

            _activated = true;
            Destroy(gameObject, _destroyTime);
        }

        protected override void OnExit(IConsumer consumer) { }
    }
}