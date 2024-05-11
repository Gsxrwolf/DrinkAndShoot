using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private FloatValue HealthValue;

    private void Start()
    {
        this.HealthValue.SetValue(10);
    }

    public void AddHealth(float health)
    {
        this.HealthValue.AddValue(health);

        // Cap the value to 100
        if (this.HealthValue.GetValue() + health is > 100) this.HealthValue.SetValue(100);
        // Normal state
        else if (this.HealthValue.GetValue() + health is > 0 and < 100) this.HealthValue.AddValue(health);
    }
}