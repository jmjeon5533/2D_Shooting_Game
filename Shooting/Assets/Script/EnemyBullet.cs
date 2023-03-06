using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float MoveSpeed;
    public int Damage;
    void Start()
    {
        Destroy(gameObject,4);
    }
    void Update()
    {
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage(Damage);
            Destroy(gameObject);
        }
    }
}
