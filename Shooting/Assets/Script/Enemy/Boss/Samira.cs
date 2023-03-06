using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samira : EnemyBase
{
    float dir = 0;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Randomfire());
        StartCoroutine(Randomfire());
    }
    protected override void Update()
    {
        base.Update();
    }
    IEnumerator Randomfire()
    {
        float dir = Random.Range(0,360);
        Instantiate(BulletPrefab,transform.position,Quaternion.Euler(0,0,dir));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Randomfire());
    }
    protected override void Dead()
    {
        base.Dead();
    }
    protected override void Move()
    {
        
    }
}
