using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AbilityRoll : MonoBehaviour
    {
        Movement _movement;
        Animator _animator;
        [SerializeField] InputManager inputManager;

        [SerializeField] float rollSpeedMultiplier = 2.3f;
        [SerializeField] float rollDuration = 0.4f;

        bool _onCooldown;
        [SerializeField] float cooldown = 0.6f;

        void Awake()
        {
            _movement = GetComponent<Movement>();
            _animator = GetComponent<Animator>();
        }

        public void Perform()
        {
            if (_onCooldown) return;
            
            _movement.MultiplyMaxSpeed(rollSpeedMultiplier);
            inputManager.DisableControls();
            _animator.SetTrigger("Roll");

            _onCooldown = true;

            StartCoroutine(PerformRoutine());
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
