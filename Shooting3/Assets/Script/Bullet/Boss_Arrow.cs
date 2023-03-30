using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Arrow : BulletBase
{
    public Vector2[] bezierPosition = new Vector2[3];

    private void Start()
    {
        bezierPosition[3] = GameManager.instance.player.transform.position;
    }
    protected override void HitFunction(Collider2D col)
    {
        Destroy(gameObject);
    }
    protected override void Move()
    {
        
    }
}
