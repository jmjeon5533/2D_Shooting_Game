using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string Name;
    public int Score;
}
public class SaveList
{
    List<SaveData> SaveDatas = new List<SaveData>();
}
public class GameManager : MonoBehaviour
{
    public SaveList save = new SaveList();
    public static GameManager instance = new GameManager();
    public PlayerController player;


    [Header("스테이지")]

    [Tooltip("스테이지 번호")]
    public int StageIndex;

    [Tooltip("스테이지 스크립트")]
    public List<StageBase> stages = new List<StageBase>();

    [Tooltip("스테이지 번호에 따른 현재 스테이지 스크립트")]
    StageBase curStage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
