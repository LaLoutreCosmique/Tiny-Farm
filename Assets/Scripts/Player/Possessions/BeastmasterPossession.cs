using UnityEngine;

namespace Player.Possessions
{
    public class BeastmasterPossession : BasePossession
    {
        GameObject _beastmaster;
        Movement _movement;

        Vector2 _movementInput;

        public override void EnterPossession(PossessionManager player, GameObject beastmaster)
        {
            if (this._beastmaster == null)
            {
                this._beastmaster = beastmaster;
                _movement = this._beastmaster.GetComponent<Movement>();
            }

            _movement.enabled = true;
        }

        public override void LeavePossession(PossessionManager player)
        {
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