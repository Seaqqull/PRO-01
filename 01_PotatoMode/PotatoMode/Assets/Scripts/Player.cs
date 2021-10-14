﻿using System.Collections;
using PotatoMode.Input;
using UnityEngine;


namespace PotatoMode
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Constants
        private const float GROUND_CHECKING = 0.1f;
        #endregion
        
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _jumpPower;
        [Header("Dashing")]
        [SerializeField] private float _dashPower;
        [SerializeField] private float _dashTime;
        [Header("Collision")] 
        [SerializeField] private Transform _groundPivot;
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _groundMask;
        [Header("Animations")] 
        [SerializeField] private Animator _eyeAnimator;
        [SerializeField] private Animator _footAnimator;
        [Header("References")] 
        [SerializeField] private Transform _view;

        private float _movementDirection;
        private Rigidbody2D _body;

        private Coroutine _groundCheckingCoroutine;
        private bool _inCollision;
        private bool _blockInput;
        private bool _grounded;
        private bool _jump;
        private bool _dash;
        

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            if (_view == null)
                _view = transform;
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
            UpdateAnimation();
            
            if (_blockInput || !_grounded)
                return;
            _movementDirection = Mathf.Sign(InputHandler.Instance.Horizontal);
            _view.localScale = (InputHandler.Instance.Horizontal == 0.0f)
                ? _view.localScale : new Vector3(_movementDirection, 1, 1);

            
            if (InputHandler.Instance.Up)
                _jump = true;
            else if (InputHandler.Instance.Space)
                _dash = true;
        }

        private void FixedUpdate()
        {
            if (_blockInput || !_grounded)
                return;
            
            if(_jump)
            {
                _footAnimator.SetTrigger("Jump");
                
                _body.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
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
            var inMove = (_body.velocity.magnitude > Mathf.Epsilon);
            
            // Foot
            _footAnimator.SetBool("InMove", inMove);
            _footAnimator.SetBool("IsRunning", InputHandler.Instance.Shift);
            
            // Eye
            _eyeAnimator.SetBool("InMove", inMove);
            _eyeAnimator.SetBool("IsRunning", InputHandler.Instance.Shift);
        }

        private IEnumerator DashWaitRoutine()
        {
            _footAnimator.SetBool("IsDashing", true);
            _blockInput = true;
            
            yield return new WaitForSeconds(_dashTime);
            
            _footAnimator.SetBool("IsDashing", false);
            _blockInput = false;
        }
        
        private IEnumerator GroundCheckRoutine()
        {
            var waitPeriod = new WaitForSeconds(GROUND_CHECKING);
            
            while (true)
            {
                _grounded = Physics2D.Raycast(_groundPivot.position, Vector2.down, _groundDistance, _groundMask);
                _footAnimator.SetBool("InAir", !_grounded);
                yield return waitPeriod;
            }
        }
    }
}