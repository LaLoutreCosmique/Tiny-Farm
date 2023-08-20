using UnityEditor.Animations;
using UnityEngine;

namespace Characters.Enemy
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = "Enemy";
        [field: SerializeField] public string Description { get; private set; } = "Simple description";
        [field: SerializeField] public AnimatorController AnimationsSprites { get; private set; }
        [field: SerializeField] public int Health { get; private set; } = 10;
        [field: SerializeField] public AttackType AttackStyle { get; private set; }
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public float Knockback { get; private set; } = 10f;
        [field: SerializeField] public MovementType MovementStyle { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; } = 20f;

        public enum MovementType
        {
            Walk,
            Fly,
            Teleport
        }

        public enum AttackType
        {
            NoAttack,
            Melee,
            Distance 
        }
    }
}
