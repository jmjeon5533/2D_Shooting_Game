using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdFire : EnemyBase
{
    float time = 0;
    float dir;
    protected override void Update()
    {
        base.Update();
        var vec = GameManager.instance.player.transform.position - transform.position;
        var deg = Mathf.Atan2(vec.y,vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,deg + 90);
        time += Time.deltaTime;
        if(time >= 2)
        {
            time = 0;
            StartCoroutine(Fire());
            IEnumerator Fire()
            {
                for(int i = 0; i < 3; i++)
                {
                    Instantiate(BulletPrefab,transform.position,transform.rotation);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }
}
