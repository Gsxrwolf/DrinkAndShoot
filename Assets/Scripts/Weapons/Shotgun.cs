using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons
{

    public class Shotgun : ProjectileWeapon
    {
        [SerializeField]
        private int m_projectileCount = 8;
        [SerializeField]
        private float m_spread = 10.0f;

        protected override IEnumerator PerformPrimaryAction()
        {
            if (CurrentClipSize == 0)
            {
                PrimaryActionFinished();
                yield break;
            }

            Vector3 direction = Matrix4x4.Rotate(Quaternion.Euler(m_spread, 0, 0)) * Vector3.forward;
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(new Vector3(0, 0, 360 / m_projectileCount)));
            for (int i = 0; i < m_projectileCount; i++)
            {
                BaseProjectile projectile = Instantiate(m_projectilePrefab, m_muzzlePoint.position, m_muzzlePoint.rotation);
                if (projectile != null)
                {
                    projectile.transform.forward = transform.localToWorldMatrix * direction;
                    projectile.Shoot();
                }
                direction = rotationMatrix * direction;
            }
            m_audioSource.clip = m_primaryActionClip;
            m_audioSource.Play();

            m_animator.SetTrigger("Primary");

            CurrentClipSize--;

            yield return new WaitWhile(() => m_audioSource.isPlaying);

            PrimaryActionFinished();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Vector3 direction = Matrix4x4.Rotate(Quaternion.Euler(m_spread, 0, 0)) * Vector3.forward;
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(new Vector3(0, 0, 360 / m_projectileCount)));
            for (int i = 0; i < m_projectileCount; i++)
            {
                Gizmos.DrawLine(m_muzzlePoint.position, m_muzzlePoint.position + (Vector3)(transform.localToWorldMatrix * direction));
                direction = rotationMatrix * direction;
            }
        }
    }
}
