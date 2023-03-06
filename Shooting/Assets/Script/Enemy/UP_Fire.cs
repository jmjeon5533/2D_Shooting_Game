using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Fire : EnemyBase
{
    float time = 0;
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector2(transform.position.x, -6);
    }
    protected override void Update()
    {
        base.Update();
        fire(8);
    }
    void fire(int count)
    {
        var vec = GameManager.instance.player.transform.position - transform.position;
        var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            float radius = 1f;
            for(int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = transform.position + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);
                Vector2 nor = vec.normalized;
                float z = Mathf.Atan2(nor.y,nor.x) * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0,0,z + 90);

                Instantiate(BulletPrefab, pos, rot);
            }
            time = 0;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }
    }
    protected override void Move()
    {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
    }
}
