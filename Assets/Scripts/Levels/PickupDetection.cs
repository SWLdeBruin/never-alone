using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDetection : MonoBehaviour
{
    [SerializeField] private InventorySystem _inventorySystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickupableObject"))
        {
            PickupableItem item = other.GetComponent<PickupableItem>();
            CollectableScriptableObject collectable = item.GetCollectable();

            _inventorySystem.CollectItem(collectable);

            collectable.OnCollect(other.gameObject);
        }
    }
}
