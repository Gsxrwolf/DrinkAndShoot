using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Weapons;
using Random = UnityEngine.Random;

public class UI_InGameInteraction : MonoBehaviour
{
    public ProgressBar DrunkProgressBarValue => this.DrunkProgressBar;
    public Vector3 InitialCrosshairPosition {  get; private set; } = Vector3.zero;

    private VisualElement RootVisualElement;

    private ProgressBar DrunkProgressBar;
    [SerializeField] private FloatValue DrunkProgressValue;

    private Slider DrunkBalanceBar;
    [SerializeField] private FloatValue DrunkBalanceValue;

    private Label PlayerHealthUI;
    [SerializeField] private FloatValue PlayerHealth;

    private Label AmmunitionUI;
    [SerializeField] private int AmmunationAmount;

    private Label ClipsUI;
    [SerializeField] private int ClipsAmount;
    

    private ProjectileWeapon CurrentWeapon;

    private VisualElement InGameContainer;
    private VisualElement GameOverContainer;
    private Button QuitButton;
    private Button RestartButton;

    private const string HIDDEN_CONTENT_CLASS = "hidden-content";

    #region Crosshair Movement

    private VisualElement Crosshair;
    private Vector3 StartCrosshairPosition;
    private Vector3 OffsetCrosshairPosition;

    private bool IsMovementFinished = true;

    #endregion



    // Start is called before the first frame update
    void Awake()
    {
        this.RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        this.DrunkBalanceBar = this.RootVisualElement.Q<Slider>("BalanceBarSlider");
        this.DrunkProgressBar = this.RootVisualElement.Q<ProgressBar>("DrunkScaleProgressBar");
        this.Crosshair = this.RootVisualElement.Q<VisualElement>("CrosshairImage");
        this.PlayerHealthUI = this.RootVisualElement.Q<Label>("HealthValue");
        this.AmmunitionUI = this.RootVisualElement.Q<Label>("AmmuValue");
        this.ClipsUI = this.RootVisualElement.Q<Label>("ClipsValue");

        this.InGameContainer = this.RootVisualElement.Q("InGameState");
        this.GameOverContainer = this.RootVisualElement.Q("GameOverState");
        this.QuitButton = this.RootVisualElement.Q<Button>("QuitButton");
        this.RestartButton = this.RootVisualElement.Q<Button>("RestartButton");

        this.StartCrosshairPosition = this.Crosshair.transform.position;

        this.DrunkProgressValue.FOnValueSet += SetDrunkScale;

        this.DrunkBalanceValue.FOnValueSet += SetDrunkBalance;

        this.PlayerHealth.FOnValueSet += SetPlayerHealth;

        InitialCrosshairPosition = this.Crosshair.transform.position;
    }

    private void OnEnable()
    {
        this.QuitButton?.RegisterCallback<ClickEvent>(QuitToMainMenu);
        this.RestartButton?.RegisterCallback<ClickEvent>(RestartLevel);

        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerActions>().OnWeaponChanged += WeaponChange;
    }

    private void OnDisable()
    {
        this.QuitButton?.UnregisterCallback<ClickEvent>(QuitToMainMenu);
        this.RestartButton?.UnregisterCallback<ClickEvent>(RestartLevel);
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

        if (this.PlayerHealth.GetValue() <= 0)
        {
            this.PlayerHealth.FOnValueSet -= SetPlayerHealth;

            InGameContainer.AddToClassList(HIDDEN_CONTENT_CLASS);
            GameOverContainer.RemoveFromClassList(HIDDEN_CONTENT_CLASS);
            RestartButton.Focus();
        }
    }

    private void WeaponChange(ABaseWeapon oldWeapon, ABaseWeapon newWeapon)
    {
        // Remove old Weapon
        if (CurrentWeapon != null)
        {
            ((ProjectileWeapon)oldWeapon).OnSecondaryActionFinished -= ReloadFinished;
            ((ProjectileWeapon)oldWeapon).OnCurrentAmmoChanged -= AmmoChanged;
            ((ProjectileWeapon)oldWeapon).OnCurrentClipSizeChanged -= ClipSizeChanged;
        }

        // Add new Weapon
        ((ProjectileWeapon)newWeapon).OnSecondaryActionFinished += ReloadFinished;
        ((ProjectileWeapon)newWeapon).OnCurrentAmmoChanged += AmmoChanged;
        ((ProjectileWeapon)newWeapon).OnCurrentClipSizeChanged += ClipSizeChanged;

        this.CurrentWeapon = (ProjectileWeapon)newWeapon;

        this.AmmunitionUI.text = Convert.ToString(this.CurrentWeapon.CurrentAmmo);
        this.ClipsUI.text = Convert.ToString(this.CurrentWeapon.CurrentClipSize);
    }

    private void ReloadFinished(ABaseWeapon aBaseWeapon)
    {
        this.ClipsAmount = this.CurrentWeapon.CurrentAmmo;
        this.AmmunationAmount = this.CurrentWeapon.CurrentClipSize;

        this.AmmunitionUI.text = Convert.ToString(this.ClipsAmount);
        this.ClipsUI.text = Convert.ToString(this.AmmunationAmount);
    }

    private void AmmoChanged(ProjectileWeapon weapon, int remainingBullets)
    {
        this.ClipsAmount = remainingBullets;
        this.AmmunitionUI.text = Convert.ToString(this.ClipsAmount);
    }

    private void ClipSizeChanged(ProjectileWeapon weapon, int remainingClips)
    {
        this.AmmunationAmount = remainingClips;
        this.ClipsUI.text = Convert.ToString(this.AmmunationAmount);
    }

    private void QuitToMainMenu(ClickEvent _event)
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
    }

    private void RestartLevel(ClickEvent _event)
    {
        SceneLoader.Instance.LoadScene(MyScenes.Reset);
    }

    public void OnSubmit(InputAction.CallbackContext _context)
    {
        // Workaround because buttons are not reacting to submit event
        if (_context.started)
        {
            if (RootVisualElement.focusController.focusedElement == QuitButton)
            {
                QuitToMainMenu(null);
            }
            else if (RootVisualElement.focusController.focusedElement == RestartButton)
            {
                RestartLevel(null);
            }
        }
    }
}