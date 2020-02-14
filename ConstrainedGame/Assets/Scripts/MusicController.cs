using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource song;
    public int startTime;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        SetupSong();
    }

    // Update is called once per frame
    void Update()
    {
        if(!song.isPlaying)
        {
            SetupSong();
        }
    }

    private void SetupSong()
    {
        song.time = startTime;
        song.Play();
    }
}
