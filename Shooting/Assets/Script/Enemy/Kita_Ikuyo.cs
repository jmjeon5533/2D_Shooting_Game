using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kita_Ikuyo : EnemyBase
{
    Vector2 target;
    protected override void Start()
    {
        base.Start();
        target = new Vector3(transform.position.x, 0, 0);
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Dead()
    {
        base.Dead();
    }
    protected override void Move()
    {
        bool kitang = Vector3.Distance(transform.position, target) <= 0.1f;
        if (!kitang)
        {
            transform.position =
            Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
        }
        else
        {
            var vec = GameManager.instance.player.transform.position - transform.position;
            var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,deg + 90)));
            target = new Vector3(transform.position.x, 10, 0);
        }
        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }

    }
}
