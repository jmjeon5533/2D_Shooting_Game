using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float hp;
    public float MoveSpeed;
    public int damage;
    public GameObject DeathEffect;
    public GameObject SummonEffect;
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        Move();
    }
    protected abstract void Move();
    public virtual void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0) Dead();
    }
    public abstract void Dead();
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Damage(damage);
            Dead();
        }
    }
}
