using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : EnemyBase
{
    float time1, time2;
    Vector3 MovePoint;
    RaycastHit2D hit;
    [SerializeField] GameObject laserWarning;
    protected override void Start()
    {
        base.Start();
        laserWarning.SetActive(false);
        MovePoint = new Vector3(Random.Range(-4.5f, 4.5f), 4, 0);
    }
    void RandomMove()
    {
        time1 += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time1 >= Random.Range(3, 5))
        {
            MovePoint = new Vector3(Random.Range(-4.5f, 4.5f), 4, 0);
            time1 = 0;
            print(MovePoint);
        }
        if (Vector2.Distance(transform.position, MovePoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoint, MoveSpeed / 100);
        }

        if (time2 >= Random.Range(3, 8))
        {
            StartCoroutine(FireLaser());
            time2 = 0;
            IEnumerator FireLaser()
            {
                laserWarning.SetActive(true);
                yield return new WaitForSeconds(1);
                Instantiate(BulletPrefab,transform.position,Quaternion.identity);
                laserWarning.SetActive(false);
            }
        }
    }
    protected override void Update()
    {
        RandomMove();
        Dead();
    }
    protected override void Dead()
    {
        base.Dead();
    }
}
