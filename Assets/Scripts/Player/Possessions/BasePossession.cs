using UnityEngine;

namespace Player.Possessions
{
    public abstract class BasePossession
    {
        public abstract void EnterPossession(PossessionManager player, GameObject beastmaster);

        public abstract void LeavePossession(PossessionManager player);

        public abstract void UpdatePossession(PossessionManager player);

        public abstract void Move(PossessionManager player, Vector2 movementInput);

        public abstract void UseAbility(PossessionManager player);
    }
}