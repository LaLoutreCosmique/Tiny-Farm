using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        EnemyData _data;
        Animator _animator;
        Rigidbody2D _rb2d;
        [CanBeNull] public Transform _target;

        EnemyData.MovementType _movementStyle;
        float _maxSpeed;
        float _currentSpeed;

        Vector2 _movementDirection;
        bool _vagrancy;
        public bool lockMove;

        void Awake()
        {
            _data = GetComponent<EnemyDataStorage>().data;
            _animator = GetComponent<Animator>();
            _rb2d = GetComponent<Rigidbody2D>();
            _maxSpeed = _data.MaxSpeed;
            _movementStyle = _data.MovementStyle;
        }

        void FixedUpdate()
        {
            
        }
    }
}