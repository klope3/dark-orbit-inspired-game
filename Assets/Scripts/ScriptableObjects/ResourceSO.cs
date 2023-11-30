using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceSO")]
public class ResourceSO : ScriptableObject
{
    [SerializeField] private string resourceName;
    [SerializeField] private ResourceType resourceType;
    [SerializeField, Min(1)] private int weight;

    public string ResourceName
    {
        get
        {
            return resourceName;
        }
    }
    public ResourceType TypeOfResource
    {
        get
        {
            return resourceType;
        }
    }
    public int Weight
    {
        get
        {
            return weight;
        }
    }

    public enum ResourceType
    {
        Silicate,
        Carbon
    }
}
