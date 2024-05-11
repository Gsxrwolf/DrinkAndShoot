using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    [DisallowMultipleComponent]
    public class PlayerActions : MonoBehaviour
    {
        public event System.Action<ABaseWeapon, ABaseWeapon> OnWeaponChanged
        {
            add
            {
                m_onWeaponChanged -= value;
                m_onWeaponChanged += value;
            }
            remove
            {
                m_onWeaponChanged -= value;
            }
        }

        public ABaseWeapon CurrentWeapon => m_currentWeapon;

        private event System.Action<ABaseWeapon, ABaseWeapon> m_onWeaponChanged;

        private Transform m_rightWeaponSocket = null;
        private ABaseWeapon m_currentWeapon = null;

        private bool m_isShooting = false;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            m_rightWeaponSocket = GameObject.FindGameObjectWithTag("RightWeaponSocket").transform;
        }

        private void Update()
        {
            if (m_isShooting && m_currentWeapon != null)
            {
                m_currentWeapon.StartPrimaryAction();
            }
        }

        public void EquipWeapon(ABaseWeapon _weapon)
        {
            if (_weapon != null && _weapon == m_currentWeapon)
            {
                m_currentWeapon.FullRestore();
                return;
            }

            ABaseWeapon previous = m_currentWeapon;
            if (previous != null)
            {
                previous.gameObject.SetActive(false);
            }

            m_currentWeapon = _weapon;
            if (m_currentWeapon == null)
                return;
           
            m_currentWeapon.FullRestore();
            m_currentWeapon.transform.SetParent(m_rightWeaponSocket);
            
            m_currentWeapon.transform.localPosition = Vector3.zero;
            m_currentWeapon.transform.localRotation = Quaternion.identity;

            m_currentWeapon.gameObject.SetActive(true);

            m_onWeaponChanged?.Invoke(previous, m_currentWeapon);
        }

        public void OnFire(InputAction.CallbackContext _context)
        {
            if (_context.started)
            {
                m_isShooting = true;
            }
            if (_context.canceled)
            {
                m_isShooting = false;
            }
        }

        public void OnReload(InputAction.CallbackContext _context)
        {
            if (_context.started && m_currentWeapon != null)
            {
                m_currentWeapon.StartSecondaryAction();
            }
        }
    }
}
