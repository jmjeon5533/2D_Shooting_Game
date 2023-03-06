using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyBase
{
    float time1;
    public float Curtime;
    public bool Appear = true;
    int pattenNum;
    [SerializeField] GameObject LaserPlane;
    [SerializeField] Vector2[] LaserPos;
    [SerializeField] GameObject fireObject;
    [SerializeField] GameObject Sami;
    protected override void Update()
    {
        if (Appear) Move();
        else Patten();
        Dead();
    }
    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 4, 0), MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, new Vector3(0, 4, 0)) < 0.1f) Appear = false;
    }
    void Patten()
    {
        time1 += Time.deltaTime;
        if (time1 >= Curtime)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                pattenNum = Random.Range(0, 4);
                StartCoroutine(RandomPatten(pattenNum));
                time1 = 0;
            }
        }
    }
    IEnumerator RandomPatten(int num)
    {
        switch (num)
        {
            case 0:
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(LaserPlane, LaserPos[i], Quaternion.identity);
                    }
                    break;
                }
            case 1:
                {
                    int count = 20;
                    int amount = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        for (int i = 0; i < 360; i += 360 / count)
                        {
                            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, i + amount));
                        }
                        yield return new WaitForSeconds(0.3f);
                        amount += 180 / count;
                    }
                    break;
                }
            case 2:
            {
                Instantiate(fireObject,new Vector3
                (Random.Range(-4.5f, 4.5f), 6, 0),Quaternion.identity);
                Instantiate(Sami,new Vector3
                (Random.Range(-4.5f, 4.5f), Random.Range(-4, 4), 0),Quaternion.identity);
                break;
            }
            case 3:
            {
                int count = 30;
                for(int i = 0; i < 360; i += 360/count)
                {
                    Instantiate(BulletPrefab,transform.position,Quaternion.Euler(0,0,i - 90));
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            }
        }
    }
    protected override void Dead()
    {
        base.Dead();
        if (HP <= 0)
        {
            StageNum.StageNumber++;
            GameManager.instance.IsNextStage = true;
        }
    }
}
