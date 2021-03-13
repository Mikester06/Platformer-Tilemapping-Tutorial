using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinMusic()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
}
