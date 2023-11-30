using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup == null) return;

        if (pickup.ResourceSO != null)
        {
            int amountAdded = playerInventory.TryAddResource(pickup.ResourceSO, pickup.Quantity);
        }
    }
}
