using UnityEngine;

public class BeerItem : AlcoholItem
{
    private void Start()
    {
        this.AlcoholContent = 5.0f;
    }
    
    public new void Collect(GameObject collector)
    {
        base.Collect(collector);
    }
}