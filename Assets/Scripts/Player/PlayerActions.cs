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

        private event System.Action<ABaseWeapon, ABaseWeapon> m_onWeaponChanged;

        private Transform m_rightWeaponSocket = null;
        private ABaseWeapon m_currentWeapon = null;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            m_rightWeaponSocket = GameObject.FindGameObjectWithTag("RightWeaponSocket").transform;
        }

        public void EquipWeapon(ABaseWeapon _weapon)
        {
            if (_weapon != null && _weapon == m_currentWeapon)
            {
                m_currentWeapon.FullRestore();
                return;
            }

            ABaseWeapon previous = m_currentWeapon;

            m_currentWeapon = _weapon;
            m_currentWeapon.FullRestore();
            m_currentWeapon.transform.SetParent(m_rightWeaponSocket);
            
            m_currentWeapon.transform.localPosition = Vector3.zero;
            m_currentWeapon.transform.localRotation = Quaternion.identity;

            m_onWeaponChanged?.Invoke(previous, m_currentWeapon);
        }

        public void OnFire(InputAction.CallbackContext _context)
        {
            if (_context.started && m_currentWeapon != null)
            {
                Debug.Log("Starting");
                m_currentWeapon.StartAction();
            }
        }

        public void OnReload(InputAction.CallbackContext _context)
        {
            if (_context.started && m_currentWeapon != null)
            {
                m_currentWeapon.StartReload();
            }
        }
    }
}
