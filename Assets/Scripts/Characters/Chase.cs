using Characters.Player;
using UnityEngine;

namespace Characters
{
    public class Chase : MonoBehaviour
    {
        Transform _target;
        [SerializeField] GameObject master; // default chased (beastmaster)
        float _speed;
        [SerializeField] float maxDistance;
        bool _followMaster;

        Rigidbody2D _rb2D;
        ManualMovement _movement;
        ManualMovement _masterMovement;

        void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _movement = GetComponent<ManualMovement>();
            _masterMovement = master.GetComponent<ManualMovement>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_target is null) return;

            // Get direction between chaser and chased
            var pos = transform.position;
            var dir = (master.transform.position - pos).normalized;
            
            //Debug.DrawLine (pos, pos + dir, Color.red, Mathf.Infinity);

            // Adjust speed
            if (_followMaster)
            {
                if (Vector2.Distance(transform.position, _target.position) < maxDistance)
                {
                    // Short distance, stop
                    _speed = 0f;
                    dir = new Vector3(0f, 0f);
                }
                else if (Vector2.Distance(transform.position, _target.position) < maxDistance * 2f)
                {
                    // Middle distance, lower master speed
                    _speed = _movement.maxSpeed - 0.2f;
                }
                else
                {
                    // Far distance, normal follow speed
                    InitializeSpeed();
                }
            }
            
            // Move
            if (_speed > 0f)
                _rb2D.AddForce(dir * _speed );

            // Animation
            _movement.AnimateMovement(dir);
        }

        public void StartChase(Transform newTarget = null)
        {
            // newTarget is null when follow the master
            if (newTarget is null)
            {
                // Target the master
                _target = master.GetComponent<Transform>();
                _followMaster = true;
            }
            else
            {
                // Target an enemy
                _target = newTarget;
                _followMaster = false;
            }
            InitializeSpeed();
        }

        public void CancelChase()
        {
            _target = null;
        }

        private void InitializeSpeed()
        {
            // Adapt the speed when following
            // Use Manual Movement speed when chasing
            if (_followMaster)
                _speed = _masterMovement.maxSpeed * 1.2f; // Faster than master
            else
                _speed = _movement.maxSpeed; // Normal speed
        }
    }
}
