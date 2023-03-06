using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject[] BulletPrefab;
    public Collider2D[] LockOnEnemy;
    public Transform[] firePivot;
    [SerializeField] GameObject GodEffect;
    [SerializeField] GameObject LaserEffect;
    public List<GameObject> EnemySnapList = new List<GameObject>();
    Rigidbody2D rigid;
    public int DiaFireRot = 1;
    public float LaserHeight;

    public bool isSkill;
    float time = 0;
    [SerializeField] float LockRadius;
    RaycastHit2D[] hit;
    public enum FireType
    {
        front,
        diagonal
    }
    public FireType firetype = FireType.front;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!GameManager.instance.IsNextStage) Move();
        if(!isSkill) Fire();
        Skill();
        GodEffect.SetActive(GameManager.instance.IsGod);
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(h, v) * MoveSpeed;
    }
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (GameManager.instance.Level)
            {
                case 0:
                    {
                        Instantiate(BulletPrefab[0], firePivot[0].position, Quaternion.identity);
                        break;
                    }
                case 1:
                    {
                        Instantiate(BulletPrefab[0], firePivot[1].position, Quaternion.identity);
                        Instantiate(BulletPrefab[0], firePivot[2].position, Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(BulletPrefab[0], firePivot[0].position, Quaternion.identity);
                        Instantiate(BulletPrefab[0], firePivot[1].position, Quaternion.identity);
                        Instantiate(BulletPrefab[0], firePivot[2].position, Quaternion.identity);
                        break;
                    }
                case 3:
                case 4:
                    {
                        Instantiate(BulletPrefab[0], firePivot[0].position, Quaternion.identity);
                        Instantiate(BulletPrefab[0], firePivot[1].position, Quaternion.identity);
                        Instantiate(BulletPrefab[0], firePivot[2].position, Quaternion.identity);
                        Instantiate(BulletPrefab[1], firePivot[3].position, Quaternion.identity);
                        Instantiate(BulletPrefab[1], firePivot[4].position, Quaternion.identity);
                        break;
                    }


            }

        }
        else if (Input.GetKey(KeyCode.Z))
        {
            int L = 0;
            switch (GameManager.instance.Level)
            {
                case 0:
                    L = 1;
                    break;
                case 1:
                    L = 1;
                    break;
                case 2:
                    L = 2;
                    break;
                case 3:
                    L = 3;
                    break;
                case 4:
                    L = 3;
                    break;
            }
            for (int i = 0; i < L; i++)
            {
                switch (firetype)
                {
                    case FireType.front:
                        {
                            time += Time.deltaTime;
                            if (time > 0.2f)
                            {
                                firetype = FireType.diagonal;
                                time = 0;
                            }
                            break;
                        }
                    case FireType.diagonal:
                        {
                            GameObject target = null;
                            LockOnEnemy = Physics2D.OverlapCircleAll
                            (transform.position, LockRadius, LayerMask.GetMask("Enemy"));
                            EnemySnapList.Clear();
                            foreach (var item in LockOnEnemy)
                            {
                                if (!EnemySnapList.Contains(item.gameObject))
                                    EnemySnapList.Add(item.gameObject);
                            }
                            if (LockOnEnemy.Length > 0)
                            {

                                time += Time.deltaTime;
                                if (time > 0.15f)
                                {
                                    SnapBullet(target);
                                    DiaFireRot = -DiaFireRot;
                                    time = 0;
                                }
                            }
                            break;
                        }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            time = 0;
            firetype = FireType.front;
        }
    }
    private void SnapBullet(GameObject target)
    {
        GameObject bul = Instantiate(BulletPrefab[0], transform.position, Quaternion.identity);
        bul.GetComponent<Bullet>().targetList = EnemySnapList;
        bul.GetComponent<Bullet>().player = this;
        bul.GetComponent<Bullet>().target =
        bul.GetComponent<Bullet>().targetList[Random.Range(0,
        bul.GetComponent<Bullet>().targetList.Count)];
    }
    private void Skill()
    {
        var g = GameManager.instance;
        if (Input.GetKey(KeyCode.X))
        {
            isSkill = true;
            time += Time.deltaTime;
            if (g.Level + 1 >= 0 && g.XP > 0)
            {
                if (time > 0.5f)
                {
                    LaserHeight = 20f + (GameManager.instance.Level * 3);
                    hit = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.up,30);
                    Debug.DrawRay(transform.position, Vector3.up * int.MaxValue, Color.red);
                    if (hit.Length > 0)
                    {
                        foreach (var item in hit)
                        {
                            if(item.transform.GetComponent<EnemyBase>() != null)
                            {
                                item.transform.GetComponent<EnemyBase>().HP -= 50 * Time.deltaTime;
                                item.transform.GetComponent<EnemyBase>().Bright();
                            }
                            else if(item.transform.GetComponent<EnemyBullet>() != null)
                            {
                                Destroy(item.transform.gameObject);
                            }
                            else
                            {

                            }
                        }
                    }
                    GameManager.instance.XP -= 100f * Time.deltaTime;
                }
            }
            else
            {
                isSkill = false;
                LaserHeight = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            isSkill = false;
            LaserHeight = 0;
            time = 0;
        }
        LaserEffect.gameObject.transform.localScale 
        = new Vector3(Mathf.Lerp(LaserEffect.gameObject.transform.localScale.x,LaserHeight,10 * Time.deltaTime),5,0);
        if(LaserEffect.gameObject.transform.localScale.x <= 0.5f)
        {
            LaserEffect.gameObject.SetActive(false);
        }
        else
        {
            LaserEffect.gameObject.SetActive(true);
        }
    }
    public void Damage(int Damage)
    {
        if (!GameManager.instance.IsGod)
        {
            GameManager.instance.HP -= Damage;
            StartCoroutine(Flash());
            IEnumerator Flash()
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }
        }
    }
}
