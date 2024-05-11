using System;
using UnityEngine;

public class WineItem : AlcoholItem
{
    private void Start()
    {
        this.AlcoholContent = 10.0f;
    }

    public new void Collect(GameObject collector)
    {
        base.Collect(collector);
    }
}