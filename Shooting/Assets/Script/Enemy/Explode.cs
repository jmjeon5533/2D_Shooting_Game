using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : EnemyBase
{
    public int ExplodeDamage = 20;
    RaycastHit2D hit;
    protected override void Update()
    {
        base.Update();
        var vec = GameManager.instance.player.transform.position - transform.position;
        var deg = Mathf.Atan2(vec.y,vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,deg + 90);
    }
    protected override void Dead()
    {
        base.Dead();
        Collider2D hit = Physics2D.OverlapCircle(transform.position,0.7f,LayerMask.GetMask("Player"));
        if(hit == null) return;
        hit.GetComponent<Player>().Damage(ExplodeDamage);
        Instantiate(ExplodePrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    
}
