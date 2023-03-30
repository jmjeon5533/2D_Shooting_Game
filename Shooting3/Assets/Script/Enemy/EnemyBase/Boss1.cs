using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
    [SerializeField] GameObject[] Bullet;
    public float RotateSpeed;
    [Tooltip("패턴 번호")]
    public int PhaseIndex;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        Rotation();
    }
    void Rotation()
    {
        transform.GetChild(0).Rotate(0, 0, RotateSpeed * Time.deltaTime);
        transform.GetChild(1).Rotate(0, 0, -RotateSpeed * Time.deltaTime);
    }
    protected override IEnumerator Routine()
    {
        while (gameObject.activeSelf)
        {
            if (HP > MaxHP / 2)
                yield return StartCoroutine(Phase1());
            else
                yield return StartCoroutine(Phase2());
        }
    }

    private IEnumerator Phase1()
    {
        float rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                {
                    yield return StartCoroutine(Pattern1());
                    break;
                }
            case 1:
                {
                    yield return StartCoroutine(Pattern2());
                    break;
                }
            case 2:
                {
                    StartCoroutine(Pattern1());
                    yield return StartCoroutine(Pattern2());
                    break;
                }
            case 3:
                {
                    yield return StartCoroutine(Pattern3());
                    break;
                }
        }
        // pattern function
        IEnumerator Pattern1()
        {
            int Count = Random.Range(10, 30);
            for (float i = 0; i <= 360; i += 360 / (float)Count)
            {
                var dir = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad), 0);
                Vector3 pos = transform.position + dir;

                var bullet = Instantiate(Bullet[0], pos, Quaternion.Euler(0, 0, i));
                bullet.GetComponent<BulletBase>().dir = dir;
            }
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
        IEnumerator Pattern2()
        {
            float j = Random.Range(5, 10);
            for (int i = 0; i < j; i++)
            {
                var g = Instantiate(Bullet[0], transform.position, Quaternion.identity);
                g.GetComponent<BulletBase>().dir = new Vector2(i / j, -1);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 6; i++)
            {
                var g = Instantiate(Bullet[0], transform.position, Quaternion.identity);
                g.GetComponent<BulletBase>().dir = new Vector2(-i / j, -1);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
        IEnumerator Pattern3()
        {
            var g = Instantiate(Bullet[1], transform.position, Quaternion.identity);
            g.GetComponent<Boss_Arrow>().bezierPosition[0] = transform.position;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private IEnumerator Phase2()
    {
        yield return null;
    }

}
