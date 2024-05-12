using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [DisallowMultipleComponent]
    public class EnableEndEffects : MonoBehaviour
    {
        [SerializeField]
        private FloatValue m_playerHealth = null;
        [SerializeField]
        private List<GameObject> m_endEffects = new List<GameObject>();

        private void Awake()
        {
            m_playerHealth.FOnValueSet += EnableEffectsOnDeath;
        }

        private void EnableEffectsOnDeath()
        {
            if (m_playerHealth.GetValue() <= 0)
            {
                foreach (GameObject obj in m_endEffects)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
