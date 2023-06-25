using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Player
{
    public class ManualMovement : MonoBehaviour
    {
        public bool activeDebug;
        
        Rigidbody2D _rb;
        Animator _animator;

        Vector2 _movementInput;

        public float maxSpeed = 2.5f;
        [SerializeField] float acceleration = 8f, slowdown = 10f;

        float _initialMaxSpeed;
        float _currentSpeed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _initialMaxSpeed = maxSpeed;
        }

        private void FixedUpdate()
        {
            if (_movementInput.magnitude > 0f && _currentSpeed >= 0f)
            {
                _currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
                _currentSpeed -= slowdown * maxSpeed * Time.deltaTime;

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxSpeed);
            _rb.velocity = _movementInput * _currentSpeed;

            if (activeDebug)
            {
                Debug.Log(_movementInput);
            }
        }

        public void ReceiveInput(Vector2 movementInput)
        {
            _movementInput = movementInput;
            AnimateMovement(movementInput);
        }

        private void AnimateMovement(Vector2 movementInput)
        {   
            if (_animator == null) return;

            if (movementInput.magnitude > 0f)
            {
                _animator.SetBool("isMoving", true);
                _animator.SetFloat("horizontal", movementInput.x);
                _animator.SetFloat("vertical", movementInput.y);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }
        }

        public void ResetVelocity()
        {
            _movementInput.x = 0f;
            _movementInput.y = 0f;
            _rb.velocity = new Vector2();
            _animator.SetBool("isMoving", false);
        }

        public void MultiplyMaxSpeed(float value)
        {
            maxSpeed *= value;
        }

        public void ResetMaxSpeed()
        {
            maxSpeed = _initialMaxSpeed;
        }
    }
}