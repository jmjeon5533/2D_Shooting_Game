using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] Cam cam;
    [Header("스텟 관련")]
    public float hp;
    public float maxHp;

    public float gas;
    public float maxGas;

    public float MoveSpeed;
    [Space(10)]
    [Tooltip("제한 크기")]
    Vector3 borderSize;

    [Header("발사 관련")]
    public float FireCooltime;
    private float FireCurtime;

    [Space(10)]
    [Tooltip("총알 프리팹")]
    [SerializeField] GameObject[] BulletPrefab;
    [Tooltip("총알 발사 위치")]
    [SerializeField] Transform[] FirePos = new Transform[5];
    [Tooltip("무기 레벨")]
    public int FireLevel = 1;
    [Tooltip("죽음 이펙트")]
    [SerializeField] GameObject DeathEffect;
    [SerializeField] GameObject CrashEffect;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        borderSize = new Vector3(160, 0, 100);
        FireCurtime = FireCooltime;
        maxHp = hp;
        maxGas = gas;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    void Move()
    {
        int dirX = 0;
        int dirZ = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) dirX = -1;
        if (Input.GetKey(KeyCode.RightArrow)) dirX = 1;
        if (Input.GetKey(KeyCode.UpArrow)) dirZ = 1;
        if (Input.GetKey(KeyCode.DownArrow)) dirZ = -1;


        float moveX = dirX * Time.deltaTime;
        float moveY = dirZ * Time.deltaTime;

        Vector3 moveVec = new Vector3(moveX, 0, moveY) * MoveSpeed;

        if (!IsInsideBorder_X(transform.position.x + moveVec.x, 0.25f))
            moveVec.x = 0f;
        if (!IsInsideBorder_Z(transform.position.z + moveVec.z, 0.25f))
            moveVec.z = 0f;
        transform.position += moveVec;

    }
    void Fire()
    {
        FireCurtime += Time.deltaTime;
        if (FireCurtime >= FireCooltime)
        {
            FireCurtime = 0;
            StartCoroutine(FireBullet());
        }
    }
    public void Damage(int damage)
    {
        hp -= damage;
        print(hp);
        if (hp <= 0) Death();

    }
    public void Death()
    {
        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        rigid.useGravity = true;
        cam.Isdeath = true;
        for(int i = 0; i < 10; i++)
        {
            Instantiate(DeathEffect,transform.position + Random.insideUnitSphere * 10f, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator CrashLanding()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(CrashEffect, transform.position + new Vector3(0, 5, 0) + Random.insideUnitSphere * 10f, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(CrashLanding());
        }
    }
    IEnumerator FireBullet()
    {
        if(FireLevel >= 4)
        {
            Instantiate(BulletPrefab[1], FirePos[0].position, Quaternion.identity);
            Instantiate(BulletPrefab[0], FirePos[1].position, Quaternion.identity);
            Instantiate(BulletPrefab[0], FirePos[2].position, Quaternion.identity);
            Instantiate(BulletPrefab[1], FirePos[3].position, Quaternion.identity);
            Instantiate(BulletPrefab[1], FirePos[4].position, Quaternion.identity);
        }
        else if (FireLevel >= 3)
        {
            Instantiate(BulletPrefab[1], FirePos[0].position, Quaternion.identity);
            Instantiate(BulletPrefab[0], FirePos[1].position, Quaternion.identity);
            Instantiate(BulletPrefab[0], FirePos[2].position, Quaternion.identity);
        }
        else if(FireLevel >= 2)
        {
            Instantiate(BulletPrefab[0], FirePos[1].position, Quaternion.identity);
            Instantiate(BulletPrefab[0], FirePos[2].position, Quaternion.identity);
        }
        else if(FireLevel >= 1)
        {
            Instantiate(BulletPrefab[0], FirePos[0].position, Quaternion.identity);
        }

        yield return null;
    }
    bool IsInsideBorder_X(float x, float radius)
    {
        return borderSize.x / 2f >= x + radius && -borderSize.x / 2f <= x - radius;
    }
    bool IsInsideBorder_Z(float z, float radius)
    {
        return borderSize.z / 2f >= z + radius && -borderSize.z / 2f <= z - radius;
    }
}
