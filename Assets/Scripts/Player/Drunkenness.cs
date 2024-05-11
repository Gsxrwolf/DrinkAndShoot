using UnityEngine;

public class Drunkenness : MonoBehaviour
{
    private FloatValue DrunkennessValue;

    public void AddDrunkenness(float drunkenness)
    {
        this.DrunkennessValue.AddValue(drunkenness);
    }
}