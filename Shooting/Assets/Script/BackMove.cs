using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : MonoBehaviour
{
    [SerializeField] Transform[] BackGround;
    [SerializeField] float MoveSpeed;
    
    [SerializeField] float dist = 10.8f;
    private void Update()
    {
        foreach(var item in BackGround)
        {
            item.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            if(item.position.y < -dist)
            {
                var pos = item.position;
                pos.y += dist * 2;
                item.position = pos;
            }
        }
    }
}
