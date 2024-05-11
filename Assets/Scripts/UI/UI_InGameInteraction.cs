using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UI_InGameInteraction : MonoBehaviour
{
    private VisualElement RootVisualElement;

    private ProgressBar DrunkProgressBar;
    [SerializeField] private FloatValue DrunkProgressValue;

    private Slider DrunkBalanceBar;
    [SerializeField] private FloatValue DrunkBalanceValue;

    # region Crosshair Movement
    
    private VisualElement Crosshair;
    private Vector3 StartCrosshairPosition;
    private Vector3 OffsetCrosshairPosition;

    private bool IsMovementFinished = true;

    #endregion

    

    // Start is called before the first frame update
    void Start()
    {
        this.RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        this.DrunkBalanceBar = this.RootVisualElement.Q<Slider>("BalanceBarSlider");
        this.DrunkProgressBar = this.RootVisualElement.Q<ProgressBar>("DrunkScaleProgressBar");
        this.Crosshair = this.RootVisualElement.Q<VisualElement>("CrosshairImage");

        this.StartCrosshairPosition = this.Crosshair.transform.position;
        
        this.DrunkProgressValue.FOnValueSet += SetDrunkScale;
        this.DrunkProgressValue.FOnValueAddChanged += AddDrunkScale;
        
        this.DrunkBalanceValue.FOnValueSet += SetDrunkBalance;
        this.DrunkBalanceValue.FOnValueAddChanged += AddDrunkBalance;
        
    }

    private void Update()
    {
        if (this.IsMovementFinished) CalculateCrosshairMovement();
        MoveCrosshair();
    }

    /// <summary>
    /// Calculate the new Crosshair position based on the Drunk Scale value
    /// </summary>
    private void CalculateCrosshairMovement()
    {
        this.IsMovementFinished = false;
        
        float range = (this.DrunkProgressBar.value - 100) * 10;

        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(-range, range);

        this.OffsetCrosshairPosition = new Vector3(randomX, randomY, this.StartCrosshairPosition.z);
    }

    /// <summary>
    /// Move the Crosshair randomly around the center in relation to the Drunk Scale value
    /// </summary>
    private void MoveCrosshair()
    {
        Vector3 newCrosshairPosition = Vector3.Lerp(this.StartCrosshairPosition, this.OffsetCrosshairPosition, Time.deltaTime);
        
        if (Vector3.Distance(this.Crosshair.transform.position, newCrosshairPosition) < 0.1f)
        {
            this.StartCrosshairPosition = newCrosshairPosition;
            this.IsMovementFinished = true;
        }

        this.Crosshair.transform.position = newCrosshairPosition;
    }

    /// <summary>
    /// Set the Balance Bar value between -100 and 100
    /// </summary>
    private void SetDrunkBalance()
    {
        this.DrunkBalanceBar.value = this.DrunkBalanceValue.GetValue();
    }
    
    /// <summary>
    /// Add a value to the Balance Bar
    /// </summary>
    /// <param name="value">Value to add on the current Value (can be Positive and Negative)</param>
    private void AddDrunkBalance(float value)
    {
        this.DrunkBalanceBar.value += value;
    }

    /// <summary>
    /// Set the Drunk Scale value between 0 and 100
    /// </summary>
    private void SetDrunkScale()
    {
        this.DrunkProgressBar.value = this.DrunkProgressValue.GetValue();
    }
    
    /// <summary>
    /// Add a value to the Drunk Scale
    /// </summary>
    /// <param name="value">Value to add on the current Value (Value can be Positive and Negative) </param>
    private void AddDrunkScale(float value)
    {
        
        
        this.DrunkProgressBar.value += value;
    }
}