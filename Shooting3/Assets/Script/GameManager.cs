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
    public bool IsClear; //Ŭ���� ��(�� ���� ����,�̵� �Ұ�)
    public bool IsClearMove; //���� ���� �� �̵���

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
