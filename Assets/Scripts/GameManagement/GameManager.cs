using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Vector2 lastCheckpointPosition;
    public static bool canMove = false;
    public static bool canFlash = false;
    public static bool hasDied = false;
    public static bool canFlipGravity = false;
    public static bool reset = false;

    public static bool escaped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
