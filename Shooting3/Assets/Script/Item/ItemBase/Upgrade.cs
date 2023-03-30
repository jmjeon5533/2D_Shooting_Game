using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ItemBase
{
    protected override void HitFunction(Collider2D col)
    {
        GameManager.WeaponLevel++;
        Destroy(gameObject);
    }
}
