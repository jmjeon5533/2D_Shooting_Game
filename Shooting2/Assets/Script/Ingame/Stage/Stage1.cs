using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : StageBase
{
    IEnumerator Wave1()
    {
        for(int i = 0; i < 4; i++)
        {
            //Instantiate(enemies[0],)
            yield return new WaitForSeconds(1);
        }
    }

}
