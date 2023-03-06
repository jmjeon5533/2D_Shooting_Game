using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = new UIManager();

    [SerializeField] Image FadePanel;
    [SerializeField] GameObject HowPlay;
    [SerializeField] GameObject RankPanel;
    [SerializeField] GameObject[] HowPlayPage;
    public Save save = new Save();

    bool IsHowPlay = false;
    bool IsRankPanel = false;
    private void Start()
    {
        StartCoroutine(FadeOut());
        SaveLoad();
    }
    public void SaveLoad()
    {
        var s = PlayerPrefs.GetString("SaveData", "null");
        if (s.Equals("null") || string.IsNullOrEmpty(s))
        {
            save = new Save();
        }
        else
        {
            save = JsonUtility.FromJson<Save>(s);
        }
    }
    private void Awake()
    {
        instance = this;
        FadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
        HowPlay.SetActive(IsHowPlay);
        RankPanel.SetActive(IsRankPanel);
    }
    public void GameStart()
    {
        StartCoroutine(FadeLoad());
    }
    public IEnumerator FadeLoad()
    {
        var f = FadePanel.color.a;
        while (f < 1)
        {
            f += 0.05f;
            FadePanel.color = new Color(0, 0, 0, f);
            yield return new WaitForSeconds(0.025f);
        }
        SceneManager.LoadScene("Main");
    }
    public IEnumerator FadeOut()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        GameManager.instance.StageText.text = $"Stage {StageNum.StageNumber + 1}";
        var f = FadePanel.color.a;
        f = 1;
        while (f > 0)
        {
            f -= 0.05f;
            FadePanel.color = new Color(0, 0, 0, f);
            yield return new WaitForSeconds(0.025f);
        }
    }
    public void HowToPlay()
    {
        IsHowPlay = !IsHowPlay;
        HowPlay.SetActive(IsHowPlay);
        PlayPage(0);
    }
    public void GameOver()
    {
        Application.Quit();
    }
    public void PlayPage(int num)
    {
        for (int i = 0; i < HowPlayPage.Length; i++)
        {
            HowPlayPage[i].SetActive(false);
        }
        HowPlayPage[num].SetActive(true);
    }
    public void RankingButton()
    {
        IsRankPanel = !IsRankPanel;
        Ranking();
    }
    void Ranking()
    {
        RankPanel.SetActive(IsRankPanel);
        List<PlayerData> PlayerList = save.PlayerData.OrderByDescending(item => item.Score).ToList();
        for(int i = 0; i < 5; i++)
        {
            RankPanel.transform.GetChild(i).GetComponent<Text>().text
            = "기록 없음";
        }
        for (int i = 0; i < RankPanel.transform.childCount; i++)
        {
            if (i > PlayerList.Count - 1 || i > 4) break;
            RankPanel.transform.GetChild(i).GetComponent<Text>().text
            = $"{i + 1}위  {PlayerList[i].Name} | {PlayerList[i].Score}점";
        }
    }
    public void RankingReset()
    {
        PlayerPrefs.DeleteAll();
        SaveLoad();
        Ranking();
    }
}
