using UnityEngine;

public enum EAlcoholType
{
    Beer,
    Wine,
    Vodka
}

public abstract class AlcoholItem : MonoBehaviour, ICollectable
{
    protected EAlcoholType AlcoholType;
    protected float AlcoholContent;

    public void Collect(GameObject collector)
    {
        Drunkenness drunkComponent = collector.GetComponent<Drunkenness>();

        if (drunkComponent != null) drunkComponent.AddDrunkenness(this.AlcoholContent);    
    }
}