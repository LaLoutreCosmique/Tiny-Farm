using System;
using System.Collections;
using Characters.Player;
using UnityEngine;

namespace Characters
{
    public class Health : MonoBehaviour
    {
        Rigidbody2D _rb2d;
        Animator _animator;
        ManualMovement _movement;

        [SerializeField] int maxHealth = 10;
        [SerializeField] int currentHealth;
        [SerializeField] float invincibleDuration = 2f;
        bool _isInvincible;

        bool _asPlayer;

        void Start()
        {
            currentHealth = maxHealth;

            if (CompareTag("Player"))
            {
                _movement = GetComponent<ManualMovement>();
                _asPlayer = true;
            }

            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            Vector2 dir = (transform.position - other.transform.position).normalized;
            Hurt(5, 30f, dir);
        }

        void Hurt(int damage, float kbForce = 0f, Vector2 kbDir = new Vector2())
        {
            if (_isInvincible) return;
            
            currentHealth -= damage;
            
            // Knockback
            _rb2d.AddForce(kbDir * kbForce, ForceMode2D.Impulse);
            
            // Animations
            // Hit animation
            _animator.SetTrigger("Hit");
            
            // Death
            if (IsDead())
            {
                _animator.SetTrigger("Death");
                EnableInvincibility(Mathf.Infinity);
                if (_asPlayer) _movement.locked = true;
            }

            EnableInvincibility(invincibleDuration);
        }

        public void EnableInvincibility(float duration)
        {
            if (_isInvincible) return;
            StartCoroutine(InvincibleRoutine(duration));
        }

        public IEnumerator InvincibleRoutine(float duration)
        {
            _isInvincible = true;
            Debug.Log(_isInvincible);
            yield return new WaitForSeconds(duration);
            _isInvincible = false;
            Debug.Log(_isInvincible);
        }

        bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}
