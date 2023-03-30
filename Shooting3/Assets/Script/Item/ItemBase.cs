using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2 dir;
    private Collider2D[] hit;
    [SerializeField] Vector2 Size;
    public string[] HitTag;

    protected virtual void Update()
    {
        transform.Translate(dir * MoveSpeed * Time.deltaTime, Space.World);
        Trigger();
    }
    void Trigger()
    {
        hit = Physics2D.OverlapBoxAll(transform.position,Size,0);
        foreach(var item in hit)
        {
            foreach(var tag in HitTag)
            {
                if (item.CompareTag(tag))
                {
                    HitFunction(item); ;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Size);
    }
    protected abstract void HitFunction(Collider2D col);
}
