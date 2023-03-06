using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;
    public List<GameObject> targetList = new List<GameObject>();
    public GameObject target;
    public Player player;
    public float T = 0;
    public float NextT = 0;
    public int rotIndex;
    Vector2 RandomValue;
    void Start()
    {
        if (target != null)
        {
            rotIndex = player.DiaFireRot;
            RandomValue = Vector2.up * Random.Range(-0.9f,0.9f);
        } 
        Destroy(gameObject, 4);
    }
    void Update()
    {
        if (target == null)
        {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 target1 = player.transform.position;
            Vector2 target2 = (Vector2)transform.position + (Vector2.right * 0.5f * rotIndex) + RandomValue;
            Vector2 target3 = target2 + Vector2.up * 0.5f * rotIndex;
            Vector2 target4 = target.transform.position;
            Vector2 pos = calculatebezier(target1, target2, target3, target4, T);
            Vector2 Nextpos = calculatebezier(target1, target2, target3, target4, T + Time.deltaTime);
            
            Vector3 vec = Nextpos - pos;
            float deg = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, deg - 90);

            transform.position = pos;
            T = Mathf.MoveTowards(T, 1, 1.5f * Time.deltaTime);
        }

    }
    Vector3 calculatebezier(Vector2 A, Vector2 B, Vector3 C, Vector3 D, float Index)
    {
        Vector2 ab = Vector2.Lerp(A, B, Index);
        Vector2 bc = Vector2.Lerp(B, C, Index);
        Vector2 cd = Vector2.Lerp(C, D, Index);
        Vector2 abbc = Vector2.Lerp(ab, bc, Index);
        Vector2 bccd = Vector2.Lerp(bc, cd, Index);
        return Vector2.Lerp(abbc, bccd, Index);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Damage(1);
            GameManager.instance.XP += 3;
            Destroy(gameObject);
            other.GetComponent<EnemyBase>().Bright();
        }
    }
}
