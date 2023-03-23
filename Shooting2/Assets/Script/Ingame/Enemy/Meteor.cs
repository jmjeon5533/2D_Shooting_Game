using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : EnemyBase
{
    [Header("¿î¼®")]
    [SerializeField] float RotateSpeed;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        MeteorRotate();
    }
    protected override void Move()
    {
        transform.position += new Vector3(0, 0, -1) * MoveSpeed * Time.deltaTime;
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
