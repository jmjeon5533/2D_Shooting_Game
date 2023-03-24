using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Header("UI")]
    public Text Stage;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        curStage = stages[StageIndex];  
        StartCoroutine(IngameLogic());
    }
    IEnumerator IngameLogic()
    {
        yield return StartCoroutine(StageFlash(StageIndex));
        yield return StartCoroutine(curStage.StageRoutine());
        //if (stageIndex < 2)
        //{
        //    TempData.Instance.stageIndex++;
        //    SceneManager.LoadScene("InGame");

        //}
        //else
        //{
        //    SceneManager.LoadScene("Ranking");
        //}
        //yield break;
    }
    IEnumerator StageFlash(int Stageindex)
    {
        Stage.text = $"Stage {Stageindex + 1}";
        float a = 0;
        while (Stage.color.a < 1)
        {
            a += 0.05f;
            Stage.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(3f);
        while(Stage.color.a > 0)
        {
            a -= 0.05f;
            Stage.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
