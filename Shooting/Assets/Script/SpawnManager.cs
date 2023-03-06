using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = new SpawnManager();
    private void Awake() {
        instance = this;
    }
    public GameObject[] SpawnPrefab;
    public GameObject[] BossPrefab;
    float time;
    public float bossTime;
    public float BossCurtime;
    [SerializeField] Image BossWarningPanel;

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        time += Time.deltaTime;
        if (!GameManager.instance.isBoss) bossTime += Time.deltaTime;
        if (time >= 2f)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                if (!GameManager.instance.isBoss)
                {
                    time = 0;
                    Vector3 RandomPos = new Vector3(Random.Range(-4.5f, 4.5f), 6, 0);
                    Instantiate(SpawnPrefab[Random.Range(0, SpawnPrefab.Length)], RandomPos, Quaternion.identity);
                }
            }
        }
        if (bossTime > BossCurtime)
        {
            StartCoroutine(WarningPanel());
            GameManager.instance.isBoss = true;
            bossTime = 0;
        }
    }
    public IEnumerator WarningPanel()
    {
        var a = BossWarningPanel.color.a;
        a = 0;
        for (int i = 0; i < 3; i++)
        {
            while (a < 0.5f)
            {
                a += 0.02f;
                BossWarningPanel.color = new Color(1, 0, 0, a);
                yield return new WaitForSeconds(0.01f);
            }
            while (a > 0.01f)
            {
                a -= 0.02f;
                BossWarningPanel.color = new Color(1, 0, 0, a);
                yield return new WaitForSeconds(0.01f);
            }
        }
        Instantiate(BossPrefab[StageNum.StageNumber], new Vector3(0, 6, 0), Quaternion.identity);
    }

}
