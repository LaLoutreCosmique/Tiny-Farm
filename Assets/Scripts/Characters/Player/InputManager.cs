using Characters.Player.Possessions;
using UnityEngine;

namespace Characters.Player
{
    public class InputManager : MonoBehaviour
    {
        PossessionManager _possessionManager;

        PlayerControls _controls;
        PlayerControls.PlayerActions _playerActions;

        bool _controlsLocked;
        Vector2 _movementInput;

        private void Awake()
        {
            _possessionManager = GetComponent<PossessionManager>();

            _controls = new PlayerControls();
            _playerActions = _controls.Player;

            _playerActions.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            _playerActions.switchPossession.performed += _ => _possessionManager.SwitchPossession();
            _playerActions.Ability.performed += _ => _possessionManager.AbilityKeyPressed();
        }

        private void Update()
        {
            if (_controlsLocked) return;
            
            //Debug.LogWarning(_possessionManager.transform.position);
            _possessionManager.ReceiveMoveInput(_movementInput);
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDestroy()
        {
            _controls.Disable();
        }

        public void EnableControls()
        {
            _controlsLocked = false;
            
            _controls.Enable();
        }
        
        public void DisableControls()
        {
            _controlsLocked = true;
            
            _movementInput.x = 0f;
            _movementInput.y = 0f;
            _controls.Disable();
        }
    }
}