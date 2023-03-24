using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : BulletBase
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
        transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            var player = other.GetComponent<PlayerController>();
            player.Damage(Damage);
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
        }
    }
}
