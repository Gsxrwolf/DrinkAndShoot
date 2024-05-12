using Managers;
using Player;
using UnityEngine;
using Weapons;

namespace Collectable
{
    [DisallowMultipleComponent]
    public class WeaponPickup : MonoBehaviour, ICollectable
    {
        public EWeaponTypes Type => m_type;

        [SerializeField]
        private EWeaponTypes m_type = EWeaponTypes.BASE;
        [SerializeField]
        private ABaseWeapon m_weapon = null;
        [SerializeField]
        private float m_rotationSpeed = 50.0f;
        [SerializeField]
        private Transform m_representation = null;

        private Vector3 m_rotationAxis = Vector3.zero;
        private WeaponPickupPool m_originPool = null;

        private void Awake()
        {
            m_rotationAxis = Random.onUnitSphere;
        }

        public void AssignPool(WeaponPickupPool _pool)
        {
            m_originPool = _pool;
        }

        public void Collect(GameObject _collector)
        {
            PlayerActions actions = _collector.GetComponent<PlayerActions>();
            if (actions != null)
            {
                actions.EquipWeapon(m_weapon);
                m_originPool.ReturnToPool(this);
            }
        }

        private void Update()
        {
            m_representation.Rotate(m_rotationAxis, m_rotationSpeed * Time.deltaTime);
        }
    }
}
