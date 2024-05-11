using Collectable;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class WeaponPickupSpawner : MonoBehaviour
    {
        [SerializeField]
        private WeaponPickupPool m_pool = null;
        [SerializeField]
        private float m_timeBetweenSpawns = 5;
        [SerializeField]
        private Vector2 m_planeSize = Vector2.zero;

        private float m_timeLeft = 0;

        private void Awake()
        {
            m_timeLeft = m_timeBetweenSpawns;
        }

        private void Update()
        {
            m_timeLeft -= Time.deltaTime;

            if (m_timeLeft < 0)
            {
                m_timeLeft = m_timeBetweenSpawns;
                WeaponPickup pickup;
                if (m_pool.GetRandomWeaponPickup(out pickup))
                {
                    if (TryGetLocation(out Vector3 position))
                    {
                        pickup.transform.position = position;
                    }
                }
            }
        }

        private bool TryGetLocation(out Vector3 _position)
        {
            float randomX = Random.Range(-m_planeSize.x / 2, m_planeSize.x / 2);
            float randomZ = Random.Range(-m_planeSize.y / 2, m_planeSize.y / 2);
            Vector3 location = transform.position + new Vector3(randomX, 0, randomZ);

            if (NavMesh.SamplePosition(location, out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas))
            {
                _position = hit.position;
                return true;
            }
            _position = Vector3.zero;
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector3(m_planeSize.x, 5, m_planeSize.y));
        }
    }
}