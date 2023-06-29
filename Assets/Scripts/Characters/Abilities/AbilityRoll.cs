using System.Collections;
using Characters.Player;
using UnityEngine;

namespace Characters.Abilities
{
    public class AbilityRoll : MonoBehaviour
    {
        ManualMovement _movement;
        Animator _animator;
        [SerializeField] InputManager inputManager;
        Health _health;

        [SerializeField] float rollSpeedMultiplier = 2.3f;
        [SerializeField] float rollDuration = 0.4f;

        bool _onCooldown;
        [SerializeField] float cooldown = 0.6f;

        void Awake()
        {
            _movement = GetComponent<ManualMovement>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        public void Perform()
        {
            if (_onCooldown) return;
            
            _movement.MultiplyMaxSpeed(rollSpeedMultiplier);
            inputManager.DisableControls();
            _animator.SetTrigger("Roll");

            _onCooldown = true;

            StartCoroutine(PerformRoutine());
            // Invincible
            _health.EnableInvincibility(rollDuration);
        }

        IEnumerator PerformRoutine()
        {
            yield return new WaitForSeconds(rollDuration);
            inputManager.EnableControls();
            _movement.ResetMaxSpeed();

            StartCoroutine(StartCooldown());
        }

        IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            _onCooldown = false;
        }
    }
}   
