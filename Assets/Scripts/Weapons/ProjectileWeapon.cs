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
            protected set
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
            protected set
            {
                if (m_currentClipSize == value)
                    return;

                m_currentClipSize = Mathf.Clamp(value, 0, m_maxClipSize);
                m_onCurrentClipSizeChanged?.Invoke(this, m_currentClipSize);
            }
        }

        [SerializeField]
        protected int m_maxAmmo = 20;
        [SerializeField]
        protected int m_maxClipSize = 5;
        [SerializeField]
        protected BaseProjectile m_projectilePrefab = null;

        // TODO: replace with animation based values
        [SerializeField]
        protected float m_timeBetweenShots = 0.5f;
        [SerializeField]
        protected float m_reloadTime = 5.0f;
        [SerializeField]
        protected Transform m_muzzlePoint = null;

        private event System.Action<ProjectileWeapon, int> m_onCurrentAmmoChanged;
        private event System.Action<ProjectileWeapon, int> m_onCurrentClipSizeChanged;

        protected int m_currentAmmo = 0;
        protected int m_currentClipSize = 0;

        protected override IEnumerator PerformPrimaryAction()
        {
            if (CurrentClipSize == 0)
            {
                PrimaryActionFinished();
                yield break;
            }

            BaseProjectile projectile = Instantiate(m_projectilePrefab, m_muzzlePoint.position, m_muzzlePoint.rotation);
            if (projectile == null)
            {
                PrimaryActionFinished();
                yield break;
            }

            m_audioSource.clip = m_primaryActionClip;
            m_audioSource.Play();
            m_animator?.SetTrigger("Primary");
            projectile.Shoot();
            CurrentClipSize--;

            yield return new WaitWhile(() => m_audioSource.isPlaying);

            PrimaryActionFinished();
        }

        protected override IEnumerator PerformSecondaryAction()
        {
            if (CurrentAmmo == 0)
            {
                SecondaryActionFinished();
                yield break;
            }

            m_audioSource.clip = m_secondaryActionClip;
            m_audioSource.Play();
            m_animator?.SetTrigger("Secondary");

            yield return new WaitWhile(() => m_audioSource.isPlaying);

            int newClipSize = CurrentAmmo >= m_maxClipSize ? m_maxClipSize : CurrentAmmo;

            CurrentClipSize = newClipSize;
            CurrentAmmo -= newClipSize;

            SecondaryActionFinished();
        }

        public override void FullRestore()
        {
            CurrentAmmo = m_maxAmmo;
            CurrentClipSize = m_maxClipSize;
        }

        public override void Reset()
        {
            base.Reset();
            m_animator?.SetTrigger("Reset");
        }
    }
}
