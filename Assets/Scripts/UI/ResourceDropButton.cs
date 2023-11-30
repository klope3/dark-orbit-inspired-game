using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDropButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private ResourceSO resourceSO;
    private Inventory playerInventory;
    private int dropAmount;

    public void SetData(ResourceSO resourceSO, int dropAmount, Inventory playerInventory)
    {
        this.resourceSO = resourceSO;
        this.dropAmount = dropAmount;
        this.playerInventory = playerInventory;
    }

    public void DoClick()
    {
        playerInventory.TryAddResource(resourceSO, -1 * dropAmount);
    }

    public void UpdateElements(int resourceQuantity)
    {
        button.interactable = resourceQuantity >= dropAmount;
    }
}
