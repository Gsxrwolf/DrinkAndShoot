using Collectable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class WeaponPickupPool : MonoBehaviour
    {
        [SerializeField]
        private List<WeaponPickup> m_prefabPickupsOnMap = new List<WeaponPickup>();
        [SerializeField]
        private int m_pickupAmountPerType = 5;

        private Dictionary<EWeaponTypes, Queue<WeaponPickup>> m_pickupPools = new Dictionary<EWeaponTypes, Queue<WeaponPickup>>();
        private int m_totalInPool = 0;

        private void Awake()
        {
            foreach (WeaponPickup prefab in m_prefabPickupsOnMap)
            {
                if (!m_pickupPools.ContainsKey(prefab.Type))
                {
                    m_pickupPools[prefab.Type] = new Queue<WeaponPickup>();
                }

                prefab.AssignPool(this);
                ReturnToPool(prefab);

                for (int i = 0; i < m_pickupAmountPerType - 1; i++)
                {
                    WeaponPickup tmp = Instantiate(prefab);

                    tmp.AssignPool(this);
                    ReturnToPool(tmp);
                }
            }
        }

        public bool GetRandomWeaponPickup(out WeaponPickup _pickup)
        {
            if (m_totalInPool == 0)
            {
                _pickup = null;
                return false;
            }

            int index = Random.Range(0, m_totalInPool);
            foreach (var pool in m_pickupPools)
            {
                if (index < pool.Value.Count)
                {
                    m_totalInPool--;
                    _pickup = pool.Value.Dequeue();
                    _pickup.gameObject.SetActive(true);
                    return true;
                }

                index -= pool.Value.Count;
            }

            Debug.LogError("This case should never be reached!");
            _pickup = null;
            return false;
        }

        public bool GetSpecificWeaponPickup(EWeaponTypes _type, out WeaponPickup _pickup)
        {
            if (m_pickupPools[_type].Count == 0)
            {
                _pickup = null;
                return false;
            }

            m_totalInPool--;
            _pickup = m_pickupPools[_type].Dequeue();
            _pickup.gameObject.SetActive(true);
            return true;
        }

        public void ReturnToPool(WeaponPickup _pickup)
        {
            if (_pickup == null)
                return;

            m_totalInPool++;
            _pickup.gameObject.SetActive(false);
            m_pickupPools[_pickup.Type].Enqueue(_pickup);
        }

    }
}
