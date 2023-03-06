using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float HP = 100;
    public float Maxhp;
    public float MoveSpeed;
    public GameObject ExplodePrefab;
    public GameObject BulletPrefab;

    bool isBright = false;
    protected virtual void Start()
    {
        Maxhp = HP;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        Dead();
    }
    protected virtual void Move()
    {
        transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
    }
    protected virtual void Dead()
    {
        if(HP <= 0 || Input.GetKeyDown(KeyCode.F2))
        {
            Instantiate(ExplodePrefab,transform.position,Quaternion.identity);
            GameManager.instance.EnemyDeathNum++;
            StageNum.Score += (int)Maxhp;
            Destroy(gameObject);
        }
    }
    public void Bright()
    {
        if(!isBright) StartCoroutine(Flash());
        IEnumerator Flash()
        {
            isBright = true;
            GetComponent<SpriteRenderer>().color = new Color(1,0,0);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = new Color(1,1,1);
            isBright = false;
        }
    }
    public virtual void Damage(int Damage)
    {
        HP -= Damage;
    }
}
