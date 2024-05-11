using UnityEngine;

namespace Collectable
{
    public class HealthItem : MonoBehaviour, ICollectable
    {
        private FloatValue HealthValue;

        public void Collect(GameObject collector)
        {
            Health healthComponent = collector.GetComponent<Health>();

            if (healthComponent != null) healthComponent.AddHealth(this.HealthValue.GetValue());
        }
    }
}