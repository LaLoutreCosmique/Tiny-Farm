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
        Chase _chase;

        [SerializeField] int maxHealth = 10;
        [SerializeField] int currentHealth;
        [SerializeField] float invincibleDuration = 2f;
        bool _isInvincible;

        void Start()
        {
            currentHealth = maxHealth;

            if (CompareTag("Player") || CompareTag("Ally"))
                _movement = GetComponent<ManualMovement>();
            if (CompareTag("Ally") || CompareTag("Enemy"))
                _chase = GetComponent<Chase>(); // Ally & Enemy have the Chase component

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
                _animator.SetBool("isDead", true);
                EnableInvincibility(Mathf.Infinity);
                if (CompareTag("Player") || CompareTag("Ally"))
                    _movement.locked = true;
                if (CompareTag("Ally") || CompareTag("Enemy"))
                    _chase.locked = true;
            }

            EnableInvincibility(invincibleDuration);
        }

        public void EnableInvincibility(float duration)
        {
            if (_isInvincible) return;
            StartCoroutine(InvincibleRoutine(duration));
        }

        IEnumerator InvincibleRoutine(float duration)
        {
            _isInvincible = true;
            yield return new WaitForSeconds(duration);
            _isInvincible = false;
        }

        bool IsDead()
        {
            return currentHealth <= 0;
        }
    }
}
