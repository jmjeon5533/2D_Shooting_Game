using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    public EnemyBase[] enemies;
    public EnemyBase Boss;
    public ItemBase[] items;

    public List<Func<IEnumerator>> Wave = new List<Func<IEnumerator>>();
    
    public IEnumerator StageRoutine()
    {
        foreach(var item in Wave)
        {
            yield return StartCoroutine(item?.Invoke());
        }
        yield break;
    }
}
