using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RectTransform playerIndicator;
    [SerializeField] private Transform playerLookReference;
    [SerializeField] private Vector2 gameWorldSize;
    public float temp;

    private void Update()
    {
        playerIndicator.anchoredPosition = new Vector2(playerTransform.position.x / gameWorldSize.x, playerTransform.position.z / gameWorldSize.y) * 100;
        Vector3 newAngles = playerIndicator.eulerAngles;
        newAngles.z = playerLookReference.eulerAngles.y * -1 + temp;
        playerIndicator.eulerAngles = newAngles;
    }
}
