using UnityEngine;

public class Drunkenness : MonoBehaviour
{
    [SerializeField] private FloatValue DrunkennessValue;

    private void Start()
    {
        this.DrunkennessValue.SetValue(75);
    }

    public void AddDrunkenness(float drunkenness)
    {
        // Cap the value to 100
        if (this.DrunkennessValue.GetValue() + drunkenness is > 100) this.DrunkennessValue.SetValue(100);
        // Normal state
        else if (this.DrunkennessValue.GetValue() + drunkenness is > 0 and < 100) this.DrunkennessValue.AddValue(drunkenness);
    }
}