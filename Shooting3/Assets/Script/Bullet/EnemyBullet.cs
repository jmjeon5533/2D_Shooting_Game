using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase
{
    protected override void HitFunction(Collider2D col)
    {
        Debug.Log("test!");
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if(!GameManager.instance.IsClear)
        col.GetComponent<Player>().Damage(Damage);
    }
    protected override void Move()
    {
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
    }
}
