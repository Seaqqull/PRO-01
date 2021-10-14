using System.Collections.Generic;
using PotatoMode.Platforms.Data;
using System.Collections;
using UnityEngine;


namespace PotatoMode.Platforms
{
    public class TrampolinePlatform : Platform
    {
        [SerializeField] [Range(0.0f, 10.0f)] private float _impactPeriod = 1.0f;
        [SerializeField] [Range(0.0f, 128)] private float _impactPower = 1.0f;
        [SerializeField] private Vector2 _impactDirection = Vector2.up;

        private Coroutine _forceImpactCoroutine;
        private List<IConsumer> _affectors;


        private void Awake()
        {
            _affectors = new List<IConsumer>();
        }


        private IEnumerator ForceImpactRoutine()
        {
            var period = new WaitForSeconds(_impactPeriod);

            while (true)
            {
                yield return period;
                
                foreach (var affector in _affectors)
                {
                    affector.Body.AddForce(_impactDirection * _impactPower, ForceMode2D.Impulse);
                }
            }
        }


        protected override void OnEnter(IConsumer consumer)
        {
            _affectors.Add(consumer);
            
            if (_affectors.Count == 1)
                _forceImpactCoroutine = StartCoroutine(ForceImpactRoutine());
        }

        protected override void OnExit(IConsumer consumer)
        {
            _affectors.Remove(consumer);
            
            if (_affectors.Count == 0)
            {
                StopCoroutine(_forceImpactCoroutine);
                _forceImpactCoroutine = null;
            }
        }
    }
}