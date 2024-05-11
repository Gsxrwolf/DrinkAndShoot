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

    private Label PlayerHealthUI;
    [SerializeField] private FloatValue PlayerHealth;

    private Label AmmunitionUI;
    [SerializeField] private FloatValue Ammunition;

    #region Crosshair Movement

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
        this.PlayerHealthUI = this.RootVisualElement.Q<Label>("HealthValue");

        this.StartCrosshairPosition = this.Crosshair.transform.position;

        this.DrunkProgressValue.FOnValueSet += SetDrunkScale;

        this.DrunkBalanceValue.FOnValueSet += SetDrunkBalance;

        this.PlayerHealth.FOnValueSet += SetPlayerHealth;

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
    /// Set the Drunk Scale value between 0 and 100
    /// </summary>
    private void SetDrunkScale()
    {
        this.DrunkProgressBar.value = this.DrunkProgressValue.GetValue();
    }

    /// <summary>
    /// Set the Player health via the scriptable Object
    /// </summary>
    private void SetPlayerHealth()
    {
        this.PlayerHealthUI.text = Convert.ToString(this.PlayerHealth.GetValue());
    }

    private void SetAmmunition()
    {

    }
}