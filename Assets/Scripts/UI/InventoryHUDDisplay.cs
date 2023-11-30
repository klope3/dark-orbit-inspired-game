using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class InventoryHUDDisplay : SerializedMonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Dictionary<ResourceSO, TMP_Text> resourceQuantityTexts;
    [SerializeField] private TMP_Text weightReadout;

    public void UpdateResourceQuantities()
    {
        foreach (KeyValuePair<ResourceSO, TMP_Text> pair in resourceQuantityTexts)
        {
            ResourceSO resourceSO = pair.Key;
            int quantity = playerInventory.GetResourceQuantity(resourceSO);
            TMP_Text text = pair.Value;
            text.text = $"{resourceSO.ResourceName} - {quantity}";
        }

        int totalWeight = playerInventory.TotalWeight;
        int weightLimit = playerInventory.WeightLimit;
        weightReadout.text = $"{totalWeight} / {weightLimit}";
        weightReadout.color = totalWeight == weightLimit ? Color.red : Color.white;
    }
}
