using System.Collections;
using Characters.Enemy;
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
        bool _isPlayer; // Determines type of object (Player/Enemy)

        void Start()
        {
            _isPlayer = CompareTag("Player") || CompareTag("Ally");
            
            currentHealth = maxHealth;

            if (_isPlayer)
                _movement = GetComponent<ManualMovement>();
            if (CompareTag("Ally") || CompareTag("Enemy"))
                _chase = GetComponent<Chase>(); // Ally & Enemy have the Chase component

            _rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void OnCollisionStay2D(Collision2D other)
        {
            var dmg = 0;
            var kbForce = 0f;
            
            if (_isPlayer) // PLAYER GET DAMAGES 
            {
                if (!other.gameObject.CompareTag("Enemy")) return;
                
                var enemyData = other.gameObject.GetComponent<EnemyDataStorage>().data;
                dmg = enemyData.Damage;
                kbForce = enemyData.Knockback;
                    
                Vector2 kbDir = (transform.position - other.transform.position).normalized;
                Hurt(dmg, kbForce, kbDir);
            }

            else // ENEMY GET DAMAGES
            {
                if (!other.gameObject.CompareTag("PlayerAttack")) return;
                
                // GET DATA FROM PLAYER ATTACK SCRIPT
                dmg = 1;
                kbForce = 10f;
                    
                Vector2 kbDir = (transform.position - other.transform.position).normalized;
                Hurt(dmg, kbForce, kbDir);
            }
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
                if (_isPlayer)
                    _movement.locked = true;
                if (CompareTag("Ally"))
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
