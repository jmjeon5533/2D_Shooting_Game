using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = new GameManager();
    public static int WeaponLevel = 0;
    public Player player;
    public BossBase boss;
    public bool IsClear; //클리어 시(맵 제한 해제,이동 불가)
    public bool IsClearMove; //보스 연출 후 이동중

    public int CurStage = 0;
    public 
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(StageStart());
    }
    IEnumerator StageStart()
    {
        yield return StartCoroutine(stage1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
