using PotatoMode.Platforms.Data;
using UnityEngine;


namespace PotatoMode.Platforms
{
    public class MovingPlatform : Platform
    {
        private enum Direction { To, From}
        
        
        [SerializeField] private Transform _pivotFrom;
        [SerializeField] private Transform _pivotTo;
        [SerializeField] private float _speed;
        [SerializeField] private Direction _direction;

        private Transform _transform;


        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _transform.position = (Vector3.MoveTowards(_transform.position,
                (_direction == Direction.From) ? _pivotFrom.position : _pivotTo.position, _speed * Time.deltaTime));
        }

        private void LateUpdate()
        {
            if ((_direction == Direction.To) && (_transform.position == _pivotTo.position))
                _direction = Direction.From;
            else if ((_direction == Direction.From) && (_transform.position == _pivotFrom.position))
                _direction = Direction.To;
        }

        
        protected override void OnExit(IConsumer consumer)
        {
            consumer.Transform.SetParent(null);
        }
        
        protected override void OnEnter(IConsumer consumer)
        {
            consumer.Transform.SetParent(_transform);
        }
    }
}