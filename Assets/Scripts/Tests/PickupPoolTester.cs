using Collectable;
using Managers;
using UnityEngine;

namespace Test
{
    public class PickupPoolTester : MonoBehaviour
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
                WeaponPickup pickup = null;
                if (m_pool.GetRandomWeaponPickup(out pickup))
                {
                    pickup.transform.position = GetLocation();
                }
            }
        }

        private Vector3 GetLocation()
        {
            float randomX = Random.Range(-m_planeSize.x / 2, m_planeSize.x / 2);
            float randomZ = Random.Range(-m_planeSize.y / 2, m_planeSize.y / 2);

            return new Vector3(randomX, 0, randomZ);
        }
    }
}