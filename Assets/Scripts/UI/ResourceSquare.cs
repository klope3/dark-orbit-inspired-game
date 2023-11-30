using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private ResourceDropButton buttonMinus1;
    [SerializeField] private ResourceDropButton buttonMinus10;
    [SerializeField] private ResourceDropButton buttonMinus50;
    private ResourceSO resourceSO;
    private Inventory playerInventory;

    public void Setup(ResourceSO resourceSO, Inventory playerInventory)
    {
        this.playerInventory = playerInventory;
        this.resourceSO = resourceSO;

        nameText.text = resourceSO.ResourceName;

        buttonMinus1.SetData(resourceSO, 1, playerInventory);
        buttonMinus10.SetData(resourceSO, 10, playerInventory);
        buttonMinus50.SetData(resourceSO, 50, playerInventory);

        UpdateElements();
    }

    public void UpdateElements()
    {
        int resourceQuantity = playerInventory.GetResourceQuantity(resourceSO);
        quantityText.text = $"{resourceQuantity}";
        buttonMinus1.UpdateElements(resourceQuantity);
        buttonMinus10.UpdateElements(resourceQuantity);
        buttonMinus50.UpdateElements(resourceQuantity);
    }
}
