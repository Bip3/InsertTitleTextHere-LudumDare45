using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    private AudioSource myAudio;
    public AudioClip[] narrationClips;
    public List<AudioClip> Queue = new List<AudioClip>();
    void Start()
    {
        myAudio = GetComponents<AudioSource>()[0];
    }

    void Update()
    {
        if (Queue.Count >= 1)
        {
            if (myAudio.isPlaying)
            {

            }
            else
            {
                myAudio.clip = Queue[0];
                myAudio.Play();
                Queue.Remove(Queue[0]);

            }
        }
    }
    public void playNarration(int narNum)
    {
        Queue.Add(narrationClips[narNum]);
    }
    public void playSound(AudioClip sound)
    {
        myAudio.PlayOneShot(sound);
    }
}
