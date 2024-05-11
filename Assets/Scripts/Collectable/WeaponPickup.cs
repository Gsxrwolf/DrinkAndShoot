using Player;
using UnityEngine;
using Weapons;

namespace Collectable
{
    [DisallowMultipleComponent]
    public class WeaponPickup : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private ABaseWeapon m_weapon = null;
        [SerializeField]
        private float m_rotationSpeed = 50.0f;

        private Vector3 m_rotationAxis = Vector3.zero;
        private void Awake()
        {
            m_rotationAxis = Random.onUnitSphere;
        }

        public void Collect(GameObject _collector)
        {
            _collector.GetComponent<PlayerActions>()?.EquipWeapon(m_weapon);
        }

        private void Update()
        {
            transform.Rotate(m_rotationAxis, m_rotationSpeed * Time.deltaTime);
        }
    }
}
