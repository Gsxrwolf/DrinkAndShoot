using UnityEngine;

namespace Weapons.Projectiles
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField]
        private float m_movementSpeed = 20;
        [SerializeField]
        private float m_timeToKill = 10;
        [SerializeField]
        private int m_damage = 5;

        private Rigidbody m_rigidbody = null;
        
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            m_rigidbody.AddForce(transform.forward * m_movementSpeed, ForceMode.Impulse);
            Destroy(this.gameObject, m_timeToKill);
        }

        private void OnTriggerEnter(Collider _other)
        {
            AIBehavior enemy = _other.GetComponent<AIBehavior>();
            if (enemy != null)
            {
                enemy.DealDamage(m_damage);
            }
            Destroy(this.gameObject);
        }
    }
}
