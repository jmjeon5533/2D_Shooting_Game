using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet1 : BulletBase
{
    protected override void HitFunction(Collider2D col)
    {
        col.GetComponent<EnemyBase>().Damage(Damage);
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected override void Move()
    {
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
    }
}
