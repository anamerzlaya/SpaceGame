using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource[] backgroundMusic;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic[0].Stop();
        PlayMainMusic();
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySFX(int soundToPlay)
    {

        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        //if (!soundEffects[soundToPlay].isPlaying)
            soundEffects[soundToPlay].Play();

    }

    public void PlayMainMusic()
    {
        //backgroundMusic[0].Stop();
        if (!backgroundMusic[0].isPlaying)
        {
            backgroundMusic[0].Play();
        }

    }

}
