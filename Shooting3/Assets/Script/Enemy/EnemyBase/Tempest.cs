using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempest : EnemyBase
{
    void Update()
    {
        transform.Translate(new Vector3(0, -1) * MoveSpeed * Time.deltaTime);
    }
    public override void Dead()
    {
        Instantiate(ExplodeEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
