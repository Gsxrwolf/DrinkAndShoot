using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BalanceProcess : MonoBehaviour
{
    [SerializeField] private FloatValue BalanceValue;
    [SerializeField] private FloatValue DrunkennesValue;

    private bool FallDirection = false; // False to the Left, True to the right
    private float CounterValue = 0;
    private bool IsGyroActive = false;


    private void Start()
    {
        // Start 
        this.BalanceValue.SetValue(Random.Range(-1f, 1f));
        
        // Not to have a start with zero 
        if (this.BalanceValue.GetValue() == 0.0f) this.BalanceValue.SetValue(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Raw Offset without the gyro
        float scaleValue = Time.fixedDeltaTime * 0.4f * this.DrunkennesValue.GetValue() * 0.01f;

        // Calculate the scalevalue with the accordingly direction
        scaleValue *= (1 / Math.Abs(this.BalanceValue.GetValue())) * (Math.Abs(this.BalanceValue.GetValue()) - Math.Abs(this.CounterValue));

        // 
        this.FallDirection = (scaleValue <= 0) ? false : true;

        float newValue = this.BalanceValue.GetValue() * (1 + scaleValue) + Time.fixedDeltaTime;

        this.BalanceValue.SetValue(newValue);
        Debug.Log(this.BalanceValue.GetValue());
    }

    private void CalculateDegree()
    {
        if (!this.IsGyroActive) return;

        Vector3 rotationSpeed = Vector3.zero;

        Vector3 rotationDelta = rotationSpeed * Time.deltaTime;
    }

}