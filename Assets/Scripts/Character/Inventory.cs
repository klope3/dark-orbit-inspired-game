using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

//if we're only using elemental resources like silicon, carbon, etc., should we simplify resources to be measured by
//weight only, instead of quantity? (e.g. 10 kg silicon instead of 10 silicon = 1kg * 10)

//Might be good to make a generic, non-Monobehaviour Inventory class 
//so the player can have multiple inventories (for items as well as resources, etc.)
public class Inventory : MonoBehaviour
{
    [SerializeField] private int weightLimit;
    [ShowInInspector, ReadOnly] private Dictionary<ResourceSO, int> resourceQuantities;
    public UnityEvent OnChange;

    [ShowInInspector]
    public int TotalWeight
    {
        get
        {
            if (resourceQuantities == null) return 0;

            //there is definitely a LINQ way for this
            int sum = 0;
            foreach (KeyValuePair<ResourceSO, int> pair in resourceQuantities)
            {
                int thisTotalWeight = pair.Key.Weight * resourceQuantities[pair.Key];
                sum += thisTotalWeight;
            }
            return sum;
        }
    }
    public int WeightLimit
    {
        get
        {
            return weightLimit;
        }
    }

    private void Awake()
    {
        resourceQuantities = new Dictionary<ResourceSO, int>();
        OnChange?.Invoke();
    }

    [Button, FoldoutGroup("Debug")]
    /// <summary>
    /// Try to add the given quantity of the given resource. Returns the quantity actually added.
    /// </summary>
    /// <param name="resourceSO">The resource type to add.</param>
    /// <param name="amount">The quantity to add. Negative values will detract from the given resource type.</param>
    /// <returns></returns>
    public int TryAddResource(ResourceSO resourceSO, int quantity)
    {
        if (quantity == 0) return 0;

        int existingQuantity = GetResourceQuantity(resourceSO);
        int weightSpaceRemaining = weightLimit - TotalWeight; //how much "space" (weight) is left in the inventory
        int addableQuantity = weightSpaceRemaining / resourceSO.Weight; //how much of this resource could be added, given how much space is left?
        int quantityToAdd = quantity < 0 ? Mathf.Clamp(quantity, -1 * existingQuantity, quantity) : Mathf.Clamp(quantity, quantity, addableQuantity);
        bool deplete = existingQuantity + quantityToAdd == 0;

        if (existingQuantity == 0) resourceQuantities.Add(resourceSO, quantityToAdd); //if we don't currently have the resource, add it to the dictionary
        if (deplete) resourceQuantities.Remove(resourceSO); //if we're removing all of the resource, remove it from the dictionary
        if (existingQuantity > 0 && !deplete) resourceQuantities[resourceSO] += quantityToAdd;

        if (quantityToAdd != 0) OnChange?.Invoke(); 
        return quantityToAdd;
    }

    public int GetTotalWeightOfResource(ResourceSO resourceSO)
    {
        if (resourceQuantities == null) return 0;

        bool existing = resourceQuantities.TryGetValue(resourceSO, out int existingQuantity);
        if (!existing) return 0;
        return existingQuantity * resourceSO.Weight;
    }

    public int GetResourceQuantity(ResourceSO resourceSO)
    {
        if (resourceQuantities == null) return 0;

        resourceQuantities.TryGetValue(resourceSO, out int quantity);
        return quantity;
    }
}
