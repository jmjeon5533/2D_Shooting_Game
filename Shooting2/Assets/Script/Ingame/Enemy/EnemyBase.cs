using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float hp;
    public float MoveSpeed;
    public int Damage;
    public GameObject DeathEffect;
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        Move();
    }
    protected abstract void Move();
    public abstract void Dead();
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().hp -= Damage;
            Dead();
        }
    }
}
