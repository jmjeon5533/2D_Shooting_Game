using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Arrow : BulletBase
{
    public Vector2[] bezierPosition = new Vector2[4];
    int[] Bezierdir = { -1, 1 };
    float t = 0;

    private void Start()
    {
        var bezier = Random.Range(0, 2);
        var range = Random.Range(-1f, 1f);
        bezierPosition[1] = bezierPosition[0] + new Vector2(Bezierdir[bezier], range);
        bezierPosition[3] = GameManager.instance.player.transform.position;
        bezierPosition[2] = bezierPosition[3] + new Vector2(Bezierdir[bezier], range);
    }
    protected override void HitFunction(Collider2D col)
    {
        Destroy(gameObject);
    }
    protected override void Move()
    {
        transform.position = Bezier(bezierPosition, t);
        t += Time.deltaTime;
    }
    Vector2 Bezier(Vector2[] vec, float t) //좌표배열, t
    {
        var i = vec.Length - 1; //기본 반복 횟수는 선의 갯수 : 좌표의 갯수 - 1
        for (int j = i; j > 0; j--) //기본 반복 횟수만큼 반복하되 점점 숫자가 적어진다
        {
            i = j; //반복 횟수
            while (0 <= i) //i가 0보다 크거나 같을 때
            {
                //vec[i]와 vec[i+1]의 좌표를 보간해 i값에 지정
                vec[i] = Vector2.Lerp(vec[i], vec[i - 1], t); 
                i--; //보간할 지점은 인덱스가 작아져야 한다
            }
        }
        return vec[0];
    }
}
