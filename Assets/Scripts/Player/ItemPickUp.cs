using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hallo");

        ICollectable collectable = collision.GetComponent<ICollectable>();

        collectable?.Collect(this.gameObject);
    }
}