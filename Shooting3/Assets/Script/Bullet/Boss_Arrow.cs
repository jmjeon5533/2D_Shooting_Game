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
    Vector2 Bezier(Vector2[] vec, float t) //��ǥ�迭, t
    {
        var i = vec.Length - 1; //�⺻ �ݺ� Ƚ���� ���� ���� : ��ǥ�� ���� - 1
        for (int j = i; j > 0; j--) //�⺻ �ݺ� Ƚ����ŭ �ݺ��ϵ� ���� ���ڰ� ��������
        {
            i = j; //�ݺ� Ƚ��
            while (0 <= i) //i�� 0���� ũ�ų� ���� ��
            {
                //vec[i]�� vec[i+1]�� ��ǥ�� ������ i���� ����
                vec[i] = Vector2.Lerp(vec[i], vec[i - 1], t); 
                i--; //������ ������ �ε����� �۾����� �Ѵ�
            }
        }
        return vec[0];
    }
}
