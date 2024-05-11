using UnityEngine;

namespace Collectable
{
    public class HealthItem : MonoBehaviour, ICollectable
    {
        [SerializeField] private float HealthValue;

        public void Collect(GameObject collector)
        {
            if (collector.tag != "Player") return;

            Health healthComponent = collector.GetComponent<Health>();

            if (healthComponent != null) healthComponent.AddHealth(this.HealthValue);

            Destroy(this.gameObject);
        }
    }
}