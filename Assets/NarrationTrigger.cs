using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public int narrationNumber;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (narrationNumber != -1)
            {
                collision.GetComponent<audioController>().playNarration(narrationNumber);
                Destroy(this.gameObject);
                doAction(narrationNumber);
            }
            GameManager.lastCheckpointPosition = transform.position;

        }
    }
    private void doAction(int Action)
    {
        if (Action == 3)
        {
            GameManager.canFlash = true;
        }
        if (Action == 9)
        {
            GameManager.canFlipGravity = true;
        }
        if (Action == 11)
        {
            GameManager.escaped = true;
        }
    }
}
