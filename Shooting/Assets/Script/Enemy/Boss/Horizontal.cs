using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : EnemyBase
{
    [SerializeField] int DirNum;
    float time = 0;
    [SerializeField] float destroyTime = 4;
    [SerializeField] float fireCurTime = 0.2f;
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject,destroyTime);
    }
    protected override void Update()
    {
        base.Update();
        fire();
    }
    protected override void Move()
    {
        transform.Translate(Vector3.right * MoveSpeed * DirNum * Time.deltaTime);
    }
    void fire()
    {
        time += Time.deltaTime;
        if(time >= fireCurTime)
        {   
            Instantiate(BulletPrefab,transform.position,transform.rotation);
            time = 0;
        }
    }

    protected override void Dead()
    {
        base.Dead();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("horizontal"))
        {
            DirNum = -DirNum;
        }
    }
}
