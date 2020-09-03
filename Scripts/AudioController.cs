using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip ready, go, getSphere, motor, getKey, levelComplete, button, openWall, music;
    public  AudioSource audioMusic, audioFx;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Music")!=null) audioMusic = GameObject.Find("Music").GetComponent<AudioSource>();
        if(audioMusic!=null)
        {
            audioMusic.clip = music;
            if((!audioMusic.isPlaying && PlayerPrefs.GetInt("Music") != 0) || (!audioMusic.isPlaying && !PlayerPrefs.HasKey("Music"))) audioMusic.Play();
        }

        audioFx = GetComponent<AudioSource>();
        
        
    }

}
