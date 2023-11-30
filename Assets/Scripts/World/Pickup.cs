using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private ResourceSO resourceSO;
    [SerializeField, Min(1)] private int quantity;

    public ResourceSO ResourceSO
    {
        get
        {
            return resourceSO;
        }
    }
    public int Quantity
    {
        get
        {
            return quantity;
        }
    }
}
