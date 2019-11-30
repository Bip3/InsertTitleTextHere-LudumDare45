using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinSequence : MonoBehaviour
{
    Image fade;
    private void Start()
    {
        fade = GetComponentInChildren<Image>();
    }
    void FixedUpdate()
    {
        if (GameManager.escaped)
        {
            if (fade.color.a < 1)
            {
                fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + .1f * Time.fixedDeltaTime);
            }
            else
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
