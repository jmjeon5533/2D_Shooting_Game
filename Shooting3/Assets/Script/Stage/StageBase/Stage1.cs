using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : StageBase
{
    private void Start()
    {
        Wave.Add(Wave1);
        Wave.Add(Wave2);
        Wave.Add(Wave3);
        Wave.Add(Wave4);
        Wave.Add(Wave5);
    }
    IEnumerator Wave1()
    {
        var j = Random.Range(-3f, 3f);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemies[0].gameObject,new Vector3(j,5),Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
    }
    IEnumerator Wave2()
    {
        var j = Random.Range(-3f, 3f);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemies[0].gameObject, new Vector3(j, 5), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
    }
    IEnumerator Wave3()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Wave4()
    {
        yield return new WaitForSeconds(1);
    }
            IEnumerator Wave5()
    {
        print(4);
        Instantiate(Boss.gameObject, new Vector3(Random.Range(-3f, 3f), 5), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
    }
}
