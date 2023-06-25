using System.Collections;
using UnityEngine;

namespace Player
{
    public class Chase : MonoBehaviour
    {
        Transform _target;
        [SerializeField] GameObject master; // default chased (beastmaster)
        float _speed;
        [SerializeField] float maxDistance;

        Rigidbody2D rb2D;

        void Start()
        {
            _speed = master.GetComponent<ManualMovement>().maxSpeed * 1.2f;

            rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_target is null) return;

            if (Vector2.Distance(transform.position, _target.position) > maxDistance)
            {
                //transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                
                // Get direction between chaser and chased
                Vector2 pos = transform.position;
                Vector2 dir = (master.transform.position - transform.position).normalized;
                Debug.DrawLine (pos, pos + dir, Color.red, Mathf.Infinity);
                Debug.Log(dir);

                rb2D.velocity = dir * _speed;
            }
        }

        public void StartChase(Transform newTarget = null)
        {
            if (newTarget is null)
                _target = master.GetComponent<Transform>();
        }

        public void CancelChase()
        {
            _target = null;
        }
    }
}
