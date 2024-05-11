using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private FloatValue HealthValue;

    public void AddHealth(float health)
    {
        this.HealthValue.AddValue(health);
    }
}