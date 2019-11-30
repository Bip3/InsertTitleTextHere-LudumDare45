using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    movement player;
    void Start()
    {
        player = GetComponentInParent<movement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            player.grounded = true;
            player.curDoubleJumps = player.doubleJumps;
            player.canFlip = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
        
    }

}
