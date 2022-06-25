using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;


    void Update()
    {
        var camerPos = transform.position;
        camerPos.x = player.position.x;
        camerPos.y = player.position.y + 2;
        transform.position = camerPos;
    }
}
