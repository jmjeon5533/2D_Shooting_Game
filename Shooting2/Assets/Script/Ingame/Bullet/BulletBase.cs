using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [Header("ÃÑ¾Ë ±âº»")]
    [SerializeField] float MoveSpeed;
    [SerializeField] float DeathTime;
    public float Damage;
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
    protected virtual void BulletMove()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<EnemyBase>().hp -= Damage;
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            if (other.GetComponent<EnemyBase>().hp <= 0) other.GetComponent<EnemyBase>().Dead();
        }
    }
}
