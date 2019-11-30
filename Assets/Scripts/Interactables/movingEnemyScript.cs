using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingEnemyScript : MonoBehaviour
{
    public float moveTime = 2;
    private float curMoveTime;
    public float speed = 2;
    float dir = 1;
    void Start()
    {
        curMoveTime = moveTime;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * dir * speed * Time.fixedDeltaTime);
        curMoveTime -= Time.fixedDeltaTime;
        if (curMoveTime <= 0)
        {
            curMoveTime = moveTime;
            dir *= -1;
        }
    }
}
