using PotatoMode.Platforms.Data;
using PotatoMode.Managers;
using System.Collections;
using PotatoMode.Players;
using PotatoMode.Input;
using UnityEngine;


namespace PotatoMode
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IConsumer
    {
        #region Constants
        private const float GROUND_CHECK_PERIOD = 0.1f;
        #endregion

        [SerializeField] private float _movementSpeed;
        [SerializeField] [Range(0.0f, 5.0f)] private float _airMovementSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _jumpPower;
        [Header("Dashing")]
        [SerializeField] private float _dashPower;
        [SerializeField] private float _dashTime;
        [Header("Collision")]
        [SerializeField] private Transform _groundPivot;
        [SerializeField] private Collider2D _groundCollider;
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _groundMask;
        [Header("Animations")]
        [SerializeField] private Animator _eyeAnimator;
        [SerializeField] private Animator _footAnimator;
        [SerializeField] private Animator _handAnimator;
        [Header("References")]
        [SerializeField] private Transform _view;

        private float _movementDirection;
        private Transform _transform;
        private IPlayerState _state;
        private Rigidbody2D _body;

        private Coroutine _groundCheckingCoroutine;
        private bool _inCollision;
        private bool _blockInput;
        private bool _grounded;
        private bool _jump;
        private bool _dash;

        public Transform Transform { get => _transform; }
        public Rigidbody2D Body { get => _body; }

        // State properties
        public float MovementDirection
        {
            get { return _movementDirection; }
            set { _movementDirection = value; }
        }
        public Transform View
        {
            get { return _view; }
        }

        
        private void Awake()
        {
            _transform = transform;
            _body = GetComponent<Rigidbody2D>();
            if (_view == null)
                _view = transform;

            _state = new BlockInputPlayerState();
        }

        private void OnEnable()
        {
            _groundCheckingCoroutine = StartCoroutine(GroundCheckRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(_groundCheckingCoroutine);
        }

        private void Update()
        {
            if (!LevelManager.Instance.LevelStarted)
                return;

            UpdateAnimation();

            _state.OnUpdate(this);
        }

        private void FixedUpdate()
        {
            if (_blockInput || !LevelManager.Instance.LevelStarted)
                return;


            if (!_grounded)
            {
                _body.AddForce(_movementSpeed * _airMovementSpeed *
                                  (InputHandler.Instance.Horizontal * Vector2.right));
                return;
            }

            if(_jump)
            {
                _footAnimator.SetTrigger(Utilities.Constants.Animation.JUMP);
                _handAnimator.SetTrigger(Utilities.Constants.Animation.JUMP);
                
                _body.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _state = new BlockInputPlayerState();
                _grounded = false;                
                _jump = false;
                return;
            }
            if (_dash)
            {
                _body.AddForce(Mathf.Sign(_view.localScale.x) * Vector2.right * _dashPower, ForceMode2D.Impulse);
                StartCoroutine(DashWaitRoutine());
                _dash = false;
                return;
            }
            
            _body.velocity = (((InputHandler.Instance.Shift) ? _runSpeed : _movementSpeed) *
                              (InputHandler.Instance.Horizontal * Vector2.right)) +
                             new Vector2(0.0f, _body.velocity.y);
        }


        private void UpdateAnimation()
        {
            var inMove = (_body.velocity != Vector2.zero);
            
            // Foot
            _footAnimator.SetBool(Utilities.Constants.Animation.IN_MOVE, inMove);
            _footAnimator.SetBool(Utilities.Constants.Animation.IS_RUNNING, (InputHandler.Instance.Shift && inMove));
            
            // Eye
            _eyeAnimator.SetBool(Utilities.Constants.Animation.IN_MOVE, inMove);
            _eyeAnimator.SetBool(Utilities.Constants.Animation.IS_RUNNING, (InputHandler.Instance.Shift && inMove));
            
            // Hand
            _handAnimator.SetBool(Utilities.Constants.Animation.IN_MOVE, inMove);
            _handAnimator.SetBool(Utilities.Constants.Animation.IS_RUNNING, (InputHandler.Instance.Shift && inMove));
        }

        private IEnumerator DashWaitRoutine()
        {
            _footAnimator.SetBool(Utilities.Constants.Animation.IS_DASHING, true);
            _handAnimator.SetBool(Utilities.Constants.Animation.IS_DASHING, true);

            _state = new BlockInputPlayerState();
            _blockInput = true;
            
            yield return new WaitForSeconds(_dashTime);
            
            _footAnimator.SetBool(Utilities.Constants.Animation.IS_DASHING, false);
            _handAnimator.SetBool(Utilities.Constants.Animation.IS_DASHING, false);

            if(_grounded)
                _state = new GroundedPlayerState();
            _blockInput = false;
        }
        
        private IEnumerator GroundCheckRoutine()
        {
            var waitPeriod = new WaitForSeconds(GROUND_CHECK_PERIOD);
            
            while (true)
            {
                _grounded = Physics2D.BoxCast(_groundCollider.bounds.center, _groundCollider.bounds.size, 0.0f,
                    Vector2.down, 0.0f, _groundMask);

                // Check state
                if (_grounded && (_state is BlockInputPlayerState))
                    _state = new GroundedPlayerState();
                else if (!_grounded && (_state is GroundedPlayerState))
                    _state = new BlockInputPlayerState();

                    _footAnimator.SetBool(Utilities.Constants.Animation.IN_AIR, !_grounded);
                _handAnimator.SetBool(Utilities.Constants.Animation.IN_AIR, !_grounded);
                yield return waitPeriod;
            }
        }


        public void Jump()
        {
            _jump = true;
        }

        public void Dash()
        {
            _dash = true;
        }
    }
}