using PotatoMode.Platforms.Data;
using System.Collections;
using UnityEngine;


namespace PotatoMode.Platforms
{
    public class TrickyPlatform : Platform
    {
        [Header("Hide")]
        [SerializeField] private float _hideTime;
        [Header("Restore")]
        [SerializeField] private float _restoreTime;
        [SerializeField] private float _restoreWaitTime;
        [Header("Positioning")]
        [SerializeField] private float _positionChangeTime;
        [SerializeField] private Transform[] _positions;
        
        private Coroutine _changePositionCoroutine;
        private Coroutine _enablePlatformCoroutine;
        private int _currentPositionIndex = -1;
        private float _animationTime = 1.0f;
        private Transform _transform;
        private bool _isWithPlayer;
        

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            if(_positions.Length > 0)
                _changePositionCoroutine = StartCoroutine(ChangePositionRoutine());
        }

        private void OnDisable()
        {
            if (_changePositionCoroutine == null)
            {
                StopCoroutine(_changePositionCoroutine);
                _changePositionCoroutine = null;    
            }
            if (_enablePlatformCoroutine != null)
            {
                StopCoroutine(_enablePlatformCoroutine);
                _enablePlatformCoroutine = null;
            }
        }

        private void Update()
        {
            // Hide
            if (_isWithPlayer && _animationTime > 0.0f)
            {
                _animationTime -= Time.deltaTime / _hideTime;
                
                _transform.localScale = new Vector3(
                    Mathf.Lerp(0.0f, 1.0f, _animationTime), 
                    _transform.localScale.y, 
                    _transform.localScale.z
                );
                
                if (_enablePlatformCoroutine == null)
                    _enablePlatformCoroutine = StartCoroutine(EnablePlatformRoutine());
            }
            // Restore time
            else if (!_isWithPlayer && _animationTime < 1.0f)
            {
                _animationTime += Time.deltaTime / _hideTime;
                
                _transform.localScale = new Vector3(
                    Mathf.Lerp(0.0f, 1.0f, _animationTime), 
                    _transform.localScale.y, 
                    _transform.localScale.z
                );
            }
        }


        private IEnumerator EnablePlatformRoutine()
        {
            yield return new WaitForSeconds(_restoreWaitTime);
            
            _enablePlatformCoroutine = null;
            _isWithPlayer = false;
        }

        private IEnumerator ChangePositionRoutine()
        {
            var waitPeriod = new WaitForSeconds(_positionChangeTime);
            int newPositionIndex;
            
            while (true)
            {
                while (true)
                {
                    newPositionIndex = UnityEngine.Random.Range(0, _positions.Length);
                    if ((_positions.Length == 1) || (newPositionIndex != _currentPositionIndex))
                        break;
                }
                _currentPositionIndex = newPositionIndex;
                _transform.position = _positions[_currentPositionIndex].position;

                yield return waitPeriod;
            }
        }
        
        protected override void OnExit(IConsumer consumer)
        {
            _isWithPlayer = false;
        }
        
        protected override void OnEnter(IConsumer consumer)
        {
            _isWithPlayer = true;
        }
    }
}