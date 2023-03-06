using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyBase
{
    float time1;
    public float Curtime;
    public bool Appear = true;
    public bool isShield;
    int pattenNum;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject Barrier;
    [SerializeField] GameObject Warning;
    [SerializeField] GameObject BigMissile;
    [SerializeField] GameObject[] SummonPlane;
    [SerializeField] Vector2[] SummonPos;
    protected override void Start()
    {
        base.Start();
        Barrier.SetActive(false);
        Warning.SetActive(false);
    }
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
                    float radius = 1.5f;
                    float j = 60 / -2f;
                    for (int n = 0; n < 5; n++)
                    {
                        for (int i = 0; i < 360; i += 360 / 8)
                        {
                            var vec = GameManager.instance.player.transform.position - transform.position;
                            Vector3 pos = transform.position + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * radius, Mathf.Sin(i * Mathf.Deg2Rad) * radius, 0);
                            Vector2 nor = vec.normalized;
                            float z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
                            Quaternion rot = Quaternion.Euler(0, 0, z + 90 + j);

                            Instantiate(BulletPrefab, pos, rot);
                        }
                        yield return new WaitForSeconds(0.3f);
                        j += 60 / 2;
                    }
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(SummonPlane[i], SummonPos[i], SummonPlane[i].transform.rotation);
                    }
                    break;
                }
            case 2:
                {
                    Barrier.SetActive(true);
                    isShield = true;
                    yield return new WaitForSeconds(2);
                    isShield = false;
                    Barrier.SetActive(false);
                    var vec = GameManager.instance.player.transform.position - transform.position;
                    var deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                    Instantiate(shieldPrefab, transform.position, Quaternion.Euler(0, 0, deg + 90));
                    break;
                }
            case 3:
                {
                    Warning.SetActive(true);
                    yield return new WaitForSeconds(2);
                    Warning.SetActive(false);
                    Instantiate(BigMissile,transform.position,Quaternion.identity);
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
    public override void Damage(int Damage)
    {
        if (!isShield)
        {
            base.Damage(Damage);
        }
    }
}
