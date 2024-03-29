using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void BulletMove()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            var Enemy = other.GetComponent<EnemyBase>();
            Enemy.Damage(Damage);
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
        }
    }
}
