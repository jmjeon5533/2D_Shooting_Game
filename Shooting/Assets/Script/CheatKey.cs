using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatKey : MonoBehaviour
{

    void Update()
    {
        Cheat();
    }
    void Cheat()
    {
        var g = GameManager.instance;
        if(Input.GetKeyDown(KeyCode.F1)) //무적
        {
            g.IsGod = !g.IsGod;
        }
        if(Input.GetKeyDown(KeyCode.F2)) //모든 적 제거
        {
            g.KillEnemy = true;
        }
        if(Input.GetKeyDown(KeyCode.F3)) //스테이지 1로 이동
        {
            StageNum.StageNumber = 0;
            g.IsNextStage = true;
        }
        if(Input.GetKeyDown(KeyCode.F4)) //스테이지 2로 이동
        {
            StageNum.StageNumber = 1;
            g.IsNextStage = true;
        }
        if(Input.GetKeyDown(KeyCode.F5)) //메뉴로 이동
        {
            g.GameExit();
        }
        if(Input.GetKeyDown(KeyCode.F6)) //경험치 만렙
        {
            g.Level = 4;
            g.XP = g.XPGauge[g.Level];
        }
        if(Input.GetKeyDown(KeyCode.F7)) //보스 소환
        {
            SpawnManager.instance.bossTime = SpawnManager.instance.BossCurtime;
        }
    }
}
