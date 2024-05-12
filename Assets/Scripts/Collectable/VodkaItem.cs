using System;
using UnityEngine;

public class VodkaItem : AlcoholItem
{

    private void Start()
    {
        this.AlcoholContent = 30.0f;
    }

    public new void Collect(GameObject collector)
    {
        base.Collect(collector);
    }
}