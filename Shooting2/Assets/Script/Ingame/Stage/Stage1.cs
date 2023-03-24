using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : StageBase
{
    private void Start()
    {
        waveList.Add(Wave1);
        waveList.Add(Wave2);
    }
    IEnumerator Wave1()
    {
        for(int i = 0; i < 4; i++)
        {
            Instantiate(enemies[0], new Vector3(Random.Range(-80f,80f),45,110),Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(3f);
    }
    IEnumerator Wave2()
    {
        for (int i = 0; i < 4; i++)
        {
            var pos = new Vector3(Random.Range(-80f, 80f), 0, 110);
            Instantiate(enemies[1].SummonEffect, pos, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            Instantiate(enemies[1], pos, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
