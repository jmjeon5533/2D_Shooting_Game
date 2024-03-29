using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : EnemyBase
{
    [Header("�")]
    [SerializeField] float RotateSpeed;
    Vector3 PlayerPos;
    protected override void Start()
    {
        base.Start();
        PlayerPos = GameManager.instance.player.gameObject.transform.position;  
        var vec = PlayerPos - transform.position;
        transform.rotation = Quaternion.LookRotation(vec).normalized;
    }
    protected override void Update()
    {
        base.Update();
        MeteorRotate();
    }
    protected override void Move()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
    void MeteorRotate()
    {
        var m = transform.GetChild(0).gameObject;
        m.transform.Rotate(0, 1 * RotateSpeed * Time.deltaTime, 0);
        m.transform.Rotate(0, 0, 1 * RotateSpeed * Time.deltaTime);
    }
    public override void Dead()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
