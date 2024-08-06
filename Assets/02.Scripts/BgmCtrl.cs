using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmCtrl : MonoBehaviour
{
    AudioSource AudioSource;
    [SerializeField]
    AudioClip[] audioClips;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

        AudioSource.PlayOneShot(audioClips[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
    }
}
