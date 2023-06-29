using Characters.Abilities;
using UnityEngine;

namespace Characters.Player.Possessions
{
    public class BeastmasterPossession : BasePossession
    {
        GameObject _beastmaster;
        GameObject _ally;
        ManualMovement _movement;
        AbilityRoll _ability;

        Vector2 _movementInput;

        public override void EnterPossession(PossessionManager player, GameObject beastmasterObject, GameObject allyObject = null)
        {
            if (this._beastmaster == null)
            {
                // INITIALIZATION
                this._beastmaster = beastmasterObject;
                _movement = this._beastmaster.GetComponent<ManualMovement>();
                _ability = _beastmaster.GetComponent<AbilityRoll>();
                _ally = allyObject;
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