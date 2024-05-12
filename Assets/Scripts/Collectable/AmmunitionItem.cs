using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionItem : MonoBehaviour, ICollectable
{
    public void Collect(GameObject collector)
    {
        if (collector.tag != "Player") return;

        Player.PlayerActions playerActions = collector.GetComponent<Player.PlayerActions>();

        if (playerActions != null) playerActions.CurrentWeapon.FullRestore();

        Destroy(this.gameObject);
    }
}
