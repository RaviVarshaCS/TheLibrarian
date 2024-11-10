using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public List <AudioClip> AudioClips;
    private AudioSource audioSource;

    public void Awake () {
       audioSource = GetComponent <AudioSource> (); 
    }
    // Start is called before the first frame update
    public void PlaySound(string SoundName) {
        Debug.Log("Play Sound");
        foreach(var clip in AudioClips) {  
           if (clip.name.Contains (SoundName)) {
            audioSource.clip = clip;
           audioSource.Play();

           }
        }
    }
    
}
 
