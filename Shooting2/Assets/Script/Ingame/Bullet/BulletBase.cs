using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [Header("ÃÑ¾Ë ±âº»")]
    public float MoveSpeed;
    public float DeathTime;
    public int Damage;
    public GameObject DeathEffect;
    protected virtual void Start()
    {
        Destroy(gameObject, DeathTime);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        BulletMove();
    }
    protected abstract void BulletMove();
    protected abstract void OnTriggerEnter(Collider other);
}
