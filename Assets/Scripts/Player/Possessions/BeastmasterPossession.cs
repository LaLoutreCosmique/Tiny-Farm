using UnityEngine;

namespace Player.Possessions
{
    public class BeastmasterPossession : BasePossession
    {
        GameObject _beastmaster;
        Movement _movement;
        AbilityRoll _ability;

        Vector2 _movementInput;

        public override void EnterPossession(PossessionManager player, GameObject beastmaster)
        {
            if (this._beastmaster == null)
            {
                // INITIALIZATION
                this._beastmaster = beastmaster;
                _movement = this._beastmaster.GetComponent<Movement>();
                _ability = _beastmaster.GetComponent<AbilityRoll>();
            }

            _movement.enabled = true;
        }

        public override void LeavePossession(PossessionManager player)
        {
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
            if (_movementInput.magnitude > 0f)
                _ability.Perform();
        }
    }
}