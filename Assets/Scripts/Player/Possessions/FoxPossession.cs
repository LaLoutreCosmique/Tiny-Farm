using UnityEngine;

namespace Player.Possessions
{
    public class FoxPossession : BasePossession
    {
        GameObject _fox;
        ManualMovement _movement;
        Chase _chaseManager;

        Vector2 _movementInput;

        public override void EnterPossession(PossessionManager player, GameObject foxObject, GameObject masterObject = null)
        {
            if (this._fox == null)
            {
                this._fox = foxObject;
                _movement = this._fox.GetComponent<ManualMovement>();
                _chaseManager = _fox.GetComponent<Chase>();
            }
            _chaseManager.CancelChase();
            _movement.enabled = true;
        }

        public override void LeavePossession(PossessionManager player)
        {
            _chaseManager.StartChase();
            _movement.ResetVelocity();
            _movement.enabled = false;
        }

        public override void UpdatePossession(PossessionManager player)
        {

        }

        public override void Move(PossessionManager player, Vector2 movementInput)
        {
            // from MonoBehaviour's Update method
            this._movementInput = movementInput;
            _movement.ReceiveInput(this._movementInput);
        }

        public override void UseAbility(PossessionManager player)
        {

        }
    }
}