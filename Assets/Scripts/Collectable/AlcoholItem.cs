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
    [SerializeField] protected float AlcoholContent;

    public void Collect(GameObject collector)
    {
        if (collector.tag != "Player") return;

        Drunkenness drunkComponent = collector.GetComponent<Drunkenness>();

        if (drunkComponent != null) drunkComponent.AddDrunkenness(this.AlcoholContent);

        DestroyImmediate(this.gameObject);
    }
}