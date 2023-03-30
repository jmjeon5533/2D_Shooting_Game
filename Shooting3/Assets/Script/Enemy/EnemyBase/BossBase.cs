using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : EnemyBase
{
    
    protected virtual void Start()
    {
        for(float t = 0; t < 1; t += 0.1f)
        {
            Debug.Log($"{t}/{Easing.OutQuad(t)}");
        }
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        for(float t = 0; t <= 4; t += Time.deltaTime)
        {
            var startpos = new Vector3(0, 12, 0);
            var endpos = new Vector3(0, 4, 0);

            transform.position = Vector3.Lerp(startpos, endpos, Easing.OutQuad(t / 4));
            yield return null;
        }
        StartCoroutine(Routine());
    }

    protected abstract IEnumerator Routine();
    public void Dead()
    {
        StartCoroutine(DisAppear());
    }
    IEnumerator DisAppear()
    {
        yield return null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
