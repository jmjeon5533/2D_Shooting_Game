using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : EnemyBase
{

    protected virtual void Start()
    {
        GameManager.instance.boss = this;
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        for (float t = 0; t <= 4; t += Time.deltaTime)
        {
            var startpos = new Vector3(0, 12, 0);
            var endpos = new Vector3(0, 3, 0);

            transform.position = Vector3.Lerp(startpos, endpos, Easing.OutQuad(t / 4));
            yield return null;
        }
        StartCoroutine(Routine());
    }

    protected abstract IEnumerator Routine();
    public override void Damage(float Damage)
    {
        HP -= Damage;
        if(HP <= 0 && !GameManager.instance.IsClear)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        StartCoroutine(DisAppear());
    }
    IEnumerator DisAppear()
    {
        GameManager.instance.IsClear = true;
        while(transform.position.y <= 8)
        {
            Instantiate(ExplodeEffect, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.instance.IsClear = false;
        GameManager.instance.IsClearMove = true;
    }
    
    protected virtual void Update()
    {
        if (GameManager.instance.IsClear)
        {
            transform.Translate(new Vector3(0,0.8f) * Time.deltaTime);
        }
    }

}
