using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos != null)
        LerpMove();
    }
    void LerpMove()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position + new Vector3(0,13 + (-playerPos.position.z / 10),-20),0.05f);
        transform.rotation = Quaternion.Euler((15 + -playerPos.position.z / 5), 0, 0);
    }
}
