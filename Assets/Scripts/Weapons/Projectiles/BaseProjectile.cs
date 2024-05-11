using UnityEngine;

namespace Weapons.Projectiles
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField]
        private float m_movementSpeed = 20;

        private Rigidbody m_rigidbody = null;
        
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            m_rigidbody.AddForce(transform.forward * m_movementSpeed, ForceMode.Impulse);
        }
    }
}
