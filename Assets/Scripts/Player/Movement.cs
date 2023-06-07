using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        Rigidbody2D _rb;
        Animator _animator;

        Vector2 _movementInput;
        [SerializeField]
        float maxSpeed = 3f, acceleration = 8f, slowdown = 10f;
        float _currentSpeed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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
    }
}