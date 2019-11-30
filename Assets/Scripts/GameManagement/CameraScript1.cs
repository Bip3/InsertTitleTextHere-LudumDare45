using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript1 : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), .05f);
    }
}
