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


    [Header("��������")]

    [Tooltip("�������� ��ȣ")]
    public int StageIndex;

    [Tooltip("�������� ��ũ��Ʈ")]
    public List<StageBase> stages = new List<StageBase>();

    [Tooltip("�������� ��ȣ�� ���� ���� �������� ��ũ��Ʈ")]
    StageBase curStage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
