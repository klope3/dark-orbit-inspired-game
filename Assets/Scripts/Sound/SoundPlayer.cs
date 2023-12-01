using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource source;
    [SerializeField, Tooltip("Will try to find an AudioSource " +
        "with this tag on Awake.")]
    private string sourceTag;
    [SerializeField] private AudioClip[] closeSounds;
    [SerializeField] private AudioClip[] midSounds;
    [SerializeField] private AudioClip[] distantSounds;
    [SerializeField, Tooltip("If the player is at least this far away, " +
        "the sound played will be chosen from the 'mid' sounds list.")] 
    private float midDistanceCutoff;
    [SerializeField, Tooltip("If the player is at least this far away, " +
        "the sound played will be chosen from the 'distant' sounds list.")]
    private float distantDistanceCutoff;
    [SerializeField] private bool playOneShot;
    [SerializeField] private float minPitch = 1;
    [SerializeField] private float maxPitch = 1;

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            playerTransform = go.transform;
        }
        if (sourceTag != null && sourceTag.Length != 0)
        {
            GameObject go = GameObject.FindGameObjectWithTag(sourceTag);
            source = go.GetComponent<AudioSource>();
        }
    }

    [Button("Play")]
    public void Play()
    {
        AudioClip[] listToUse = closeSounds;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (midSounds != null && midSounds.Length > 0 && distanceToPlayer >= midDistanceCutoff)
        {
            listToUse = midSounds;
        }
        if (distantSounds != null && distantSounds.Length > 0 && distanceToPlayer >= distantDistanceCutoff)
        {
            listToUse = distantSounds;
        }

        if (listToUse == null || listToUse.Length == 0)
        {
            Debug.LogWarning(gameObject.name + " tried to play a sound, but was unable to find a clip to play.");
            return;
        }

        int randIndex = Random.Range(0, listToUse.Length);
        AudioClip clip = listToUse[randIndex];
        float randPitch = Random.Range(minPitch, maxPitch);
        source.pitch = randPitch;
        source.transform.position = transform.position;
        if (playOneShot) source.PlayOneShot(clip);
        else
        {
            source.clip = clip;
            source.Play();
        }
    }
}
