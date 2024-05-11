using UnityEngine;

public class Drunkenness : MonoBehaviour
{
    [SerializeField] private FloatValue DrunkennessValue;

    public void AddDrunkenness(float drunkenness)
    {
        this.DrunkennessValue.AddValue(drunkenness);
    }
}