using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons
{
    public class ProjectileWeapon : ABaseWeapon
    {
        public event System.Action<ProjectileWeapon, int> OnCurrentAmmoChanged
        {
            add
            {
                m_onCurrentAmmoChanged -= value;
                m_onCurrentAmmoChanged += value;
            }
            remove
            {
                m_onCurrentAmmoChanged -= value;
            }
        }

        public event System.Action<ProjectileWeapon, int> OnCurrentClipSizeChanged
        {
            add
            {
                m_onCurrentClipSizeChanged -= value; m_onCurrentClipSizeChanged += value;
            }
            remove
            {
                m_onCurrentAmmoChanged -= value;
            }
        }

        public int CurrentAmmo
        {
            get
            {
                return m_currentAmmo;
            }
            private set
            {
                if (m_currentAmmo == value)
                    return;

                m_currentAmmo = Mathf.Clamp(value, 0, m_maxAmmo);
                m_onCurrentAmmoChanged?.Invoke(this, m_currentAmmo);
            }
        }

        public int CurrentClipSize
        {
            get
            {
                return m_currentClipSize;
            }
            private set
            {
                if (m_currentClipSize == value)
                    return;

                m_currentClipSize = Mathf.Clamp(value, 0, m_maxClipSize);
                m_onCurrentClipSizeChanged?.Invoke(this, m_currentClipSize);
            }
        }

        [SerializeField]
        private int m_maxAmmo = 20;
        [SerializeField]
        private int m_maxClipSize = 5;
        [SerializeField]
        private BaseProjectile m_projectilePrefab = null;

        // TODO: replace with animation based values
        [SerializeField]
        private float m_timeBetweenShots = 0.5f;
        [SerializeField]
        private float m_reloadTime = 5.0f;
        [SerializeField]
        private Transform m_muzzlePoint = null;

        private event System.Action<ProjectileWeapon, int> m_onCurrentAmmoChanged;
        private event System.Action<ProjectileWeapon, int> m_onCurrentClipSizeChanged;

        private int m_currentAmmo = 0;
        private int m_currentClipSize = 0;

        protected override IEnumerator PerformAction()
        {
            if (CurrentClipSize == 0)
            {
                ActionFinished();
                yield break;
            }

            BaseProjectile projectile = Instantiate(m_projectilePrefab, m_muzzlePoint.position, m_muzzlePoint.rotation);
            if (projectile == null)
            {
                ActionFinished();
                yield break;
            }

            projectile.Shoot();
            CurrentClipSize--;

            yield return new WaitForSeconds(m_timeBetweenShots);
            ActionFinished();
        }

        protected override IEnumerator PerformReload()
        {
            if (CurrentAmmo == 0)
            {
                ReloadFinished();
                yield break;
            }

            yield return new WaitForSeconds(m_reloadTime);

            int newClipSize = CurrentAmmo >= m_maxClipSize ? m_maxClipSize : CurrentAmmo;

            CurrentClipSize = newClipSize;
            CurrentAmmo -= newClipSize;

            ReloadFinished();
        }

        public override void FullRestore()
        {
            CurrentAmmo = m_maxAmmo;
            CurrentClipSize = m_maxClipSize;
        }

        private void OnGUI()
        {
            GUILayout.Label("Ammo: " + CurrentAmmo);
            GUILayout.Label("Clip: " + CurrentClipSize);
        }
    }
}
