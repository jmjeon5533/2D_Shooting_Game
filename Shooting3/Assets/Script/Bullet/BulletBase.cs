using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] float Range;
    public float MoveSpeed;
    public float Damage;
    public Vector2 dir;
    [Tooltip("Æø¹ß ÀÌÆåÆ®")]
    public GameObject ExplosionEffect;
    [SerializeField] string[] HitTag;
    Collider2D[] hit;

    // Update is called once per frame
    void Update()
    {
        Trigger();
        Move();
        if (Mathf.Abs(transform.position.x) > 8.5f || Mathf.Abs(transform.position.y) > 5.5f) Destroy(gameObject);
    }
    void Trigger()
    {
        hit = Physics2D.OverlapCircleAll(transform.position,Range);
        foreach(var item in hit)
        {
            foreach(var tag in HitTag)
            {
                if(item.CompareTag(tag))
                {
                    HitFunction(item);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    protected abstract void HitFunction(Collider2D col);
    protected abstract void Move();
}
