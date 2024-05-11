using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DrunkProgress : MonoBehaviour, ISequentialTimeCall
{
    public float StartTime { get; set; }

    public float EndTime { get; set; }

    public float CurrentTime { get; set; }


    [SerializeField] private FloatValue DrunkennessValue;
    [SerializeField] private FloatValue PlayerCurrentMovementSpeed;
    
    private void Awake()
    {
        this.StartTime = 0;
        this.EndTime = 5;
    }

    private void Start()
    {
        InitDrunkProgress();
    }
    
    /// <summary>
    /// Initialize the Drunk Progress by setting the value to 100 and invoking the UpdateDrunkProgress method
    /// </summary>
    private void InitDrunkProgress()
    {
        if (!this.DrunkennessValue) return;
        
        this.DrunkennessValue.SetValue(100);
        
        InvokeRepeating(nameof(UpdateDrunkProgress), this.StartTime, this.EndTime);
    }
    
    /// <summary>
    /// Update the Drunk Progress by decreasing the value based on the Player's Movement Speed
    /// </summary>
    private void UpdateDrunkProgress()
    {
        float decreaseValue = (1 + 1 * this.PlayerCurrentMovementSpeed.GetValue()) * -1;
        
        if ((this.DrunkennessValue.GetValue() + decreaseValue) is < 0) this.DrunkennessValue.SetValue(0);
        else if ((this.DrunkennessValue.GetValue() + decreaseValue) is > 0 or < 100) this.DrunkennessValue.AddValue(decreaseValue * 20);
        
        
        
    }
    
    /// <summary>
    /// Increase the Drunk Progress by a given value when the Player drinks alcohol
    /// </summary>
    /// <param name="increaseValue"></param>
    private void IncreaseDrunkProgress(float increaseValue)
    {
        if (this.DrunkennessValue.GetValue() + increaseValue is > 100) this.DrunkennessValue.SetValue(100);
        else if (this.DrunkennessValue.GetValue() + increaseValue is > 0 or < 100) this.DrunkennessValue.AddValue(increaseValue);
    }
}