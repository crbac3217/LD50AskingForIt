using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OnButtonPress()
    {
        int indexRand = Random.Range(0, clips.Length);
        source.clip = clips[indexRand];
        source.Play();
    }
}
