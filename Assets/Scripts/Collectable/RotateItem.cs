using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    private Vector3 StartPosition;
    private bool AufwaertsRichtung = true;
    private float Offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InitItemPosition();
    }

    /// <summary>
    /// To save the start/Spawn position of the item
    /// </summary>
    private void InitItemPosition()
    {
        this.StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 10 * Time.deltaTime);

        float Richtung  = (AufwaertsRichtung) ? Time.deltaTime * 0.1f : Time.deltaTime * -0.1f;
        
        Vector3 vector3 = transform.position;
        vector3.x = this.StartPosition.x;
        vector3.y = transform.position.y + Richtung;
        vector3.z = this.StartPosition.z;

        if (vector3.y >= this.StartPosition.y + Offset) AufwaertsRichtung = false;
        if (vector3.y <= this.StartPosition.y - Offset) AufwaertsRichtung = true;

        transform.position = vector3;
    }
}
