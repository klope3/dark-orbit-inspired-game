using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour
{
    [SerializeField] private GameObjectPool pool;
    [SerializeField, Tooltip("Try to find the GameObjectPool with this tag" +
        " on awake.")] private string poolTag;

    private void Awake()
    {
        GameObject poolGo = GameObject.FindGameObjectWithTag(poolTag);
        pool = poolGo.GetComponent<GameObjectPool>();
        if (pool == null)
        {
            Debug.LogWarning(gameObject.name + " was unable to find a GameObjectPool to use.");
        }
    }

    public void Play()
    {
        GameObject go = pool.GetPooledObject();
        go.transform.position = transform.position;
        go.SetActive(true);
        ParticleSystem particles = go.GetComponent<ParticleSystem>();
        particles.Play();
    }
}
