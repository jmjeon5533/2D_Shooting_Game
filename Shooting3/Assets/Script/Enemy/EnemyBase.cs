using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float HP,MaxHP;
    public GameObject ExplodeEffect;
    void Update()
    {
        
    }
    public virtual void Damage(float Damage)
    {
        HP -= Damage;
    }
}
