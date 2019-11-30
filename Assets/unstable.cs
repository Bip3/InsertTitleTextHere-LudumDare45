using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unstable : MonoBehaviour
{
    GameObject player;
    float distanceToPLayer;
    bool fading;
    SpriteRenderer sprite;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.reset == true)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            fading = false;
        }
        distanceToPLayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPLayer < 5)
        {
            fading = true;
        }
        if (fading)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - .1f * Time.fixedDeltaTime);

            if (sprite.color.a <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
