using UnityEngine;

namespace Characters.Enemy
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public string EnemyName { get; private set; } = "Enemy";
        [field: SerializeField] public string Description { get; private set; } = "Simple description";
        [field: SerializeField] public GameObject EnemySprites { get; private set; }
        [field: SerializeField] public int Health { get; private set; } = 10;
        [field: SerializeField] public float MaxSpeed { get; private set; } = 2.5f;
        [field: SerializeField] public int Damage { get; private set; } = 1;
        
        public enum MovementType
        {
            Normal,
            Fly,
            Teleport
        }

        public enum AttackType
        {
            Melee,
            Distance 
        }
    }
}
