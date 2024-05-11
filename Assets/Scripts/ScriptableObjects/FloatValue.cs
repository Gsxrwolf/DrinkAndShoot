using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Float Variable", menuName = "ScriptableObjects/FloatValue")]
public class FloatValue : ScriptableObject
{
    private float Value = 0.0f;

    public event Action FOnValueSet;
    public event Action<float> FOnValueAddChanged;

    public float GetValue()
    {
        return this.Value;
    }

    public void SetValue(float newValue)
    {
        this.Value = newValue;
        
        this.FOnValueSet?.Invoke();
    }

    public void AddValue(float newValue)
    {
        Debug.Log(newValue);
        this.Value += newValue;
        
        this.FOnValueAddChanged?.Invoke(newValue);
    }
}