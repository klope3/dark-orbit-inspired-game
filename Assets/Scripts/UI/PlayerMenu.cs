using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private ResourceSquare resourceSquarePf;
    [SerializeField] private RectTransform resourceGrid;
    [SerializeField] private AllResourcesSO allResources;
    [SerializeField] private Inventory playerInventory;
    private List<ResourceSquare> resourceSquares;
    private bool initialized;

    private void Awake()
    {
        if (!initialized) Setup();
    }

    private void Setup()
    {
        resourceSquares = new List<ResourceSquare>();
        foreach (ResourceSO resourceSO in allResources.resources)
        {
            ResourceSquare newSquare = Instantiate(resourceSquarePf, resourceGrid);
            newSquare.Setup(resourceSO, playerInventory);
            resourceSquares.Add(newSquare);
        }
        initialized = true;
    }

    public void UpdateElements()
    {
        if (!initialized) Setup();

        foreach (ResourceSquare resourceSquare in resourceSquares)
        {
            resourceSquare.UpdateElements();
        }
    }
}
