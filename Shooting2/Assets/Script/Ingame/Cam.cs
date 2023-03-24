using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform playerPos;
    public bool Isdeath = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Isdeath)
        {
            if (playerPos != null)
            {
                LerpMove();
            }
        }
        else
        {
            DeathRotate();
        }
    }
    void LerpMove()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position + new Vector3(0, 13 + (-playerPos.position.z / 10), -20), 0.05f);
        transform.rotation = Quaternion.Euler((15 + -playerPos.position.z / 5), 0, 0);
    }
    public void DeathRotate()
    {
        var vec = playerPos.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vec).normalized;
    }
}
