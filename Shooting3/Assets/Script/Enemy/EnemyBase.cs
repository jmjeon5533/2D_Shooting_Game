using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float HP, MaxHP;
    public float MoveSpeed;
    public GameObject ExplodeEffect;
    private void Start()
    {
        MaxHP = HP;
    }
    public virtual void Damage(float Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
            Dead();
        }
    }
    public abstract void Dead();
}
