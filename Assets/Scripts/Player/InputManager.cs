using Player.Possessions;
using UnityEngine;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] PossessionManager _possessionManager;

        PlayerControls _controls;
        PlayerControls.PlayerActions _playerActions;

        Vector2 _movementInput;

        private void Awake()
        {
            _possessionManager = GetComponent<PossessionManager>();

            _controls = new PlayerControls();
            _playerActions = _controls.Player;

            _playerActions.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            _playerActions.switchPossession.performed += _ => _possessionManager.SwitchPossession();
        }

        private void Update()
        {
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
    }
}