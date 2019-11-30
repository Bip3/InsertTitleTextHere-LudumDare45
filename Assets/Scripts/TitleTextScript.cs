using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextScript : MonoBehaviour
{
    public string titleText;
    Text display;
    AudioSource myAudio;
    public AudioClip[] clips;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        display = GetComponent<Text>();

        StartCoroutine(toStart());
        display.text = "";
    }
    void Update()
    {
        
    }
    IEnumerator toStart()
    {
        int i = 0;
        while (i < 5)
        {
            display.text = "|";
            yield return new WaitForSeconds(.5f);
            display.text = "";
            yield return new WaitForSeconds(.5f);
            i++;
        }
        StartCoroutine(addInStart(titleText.ToCharArray()));

    }
    IEnumerator addInStart(char[] addto)
    {
        List<char> charRay = new List<char>();
        for (int i = 0; i < addto.Length; i++)
        {
            yield return new WaitForSeconds(.1f);
            display.text += addto[i];
            myAudio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
        GameManager.canMove = true;
        StartCoroutine(blinkingCursor());
    }
    IEnumerator blinkingCursor()
    {
        int i = 50;
        while (i == 50)
        {
            display.text += "|";
            yield return new WaitForSeconds(.5f);
            display.text = display.text.Substring(0, display.text.Length - 1);
            yield return new WaitForSeconds(.5f);
        }

    }
}
