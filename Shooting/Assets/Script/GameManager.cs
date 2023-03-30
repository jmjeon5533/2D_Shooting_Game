using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}
[System.Serializable]
public class Save
{
    public List<PlayerData> PlayerData = new List<PlayerData>();
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance = new GameManager();
    public Save save = new Save();
    PlayerData playerData = new PlayerData();

    private void Awake()
    {
        instance = this;
    }
    [Header("스탯")]
    public int HP, MaxHP;
    public int Gas, MaxGas;
    public float XP = 0;
    public float[] XPGauge = new float[5];
    public int Level = 0;
    public int EnemyDeathNum = 0;
    public bool isBoss;
    public bool IsUI;
    public bool IsNextStage;
    public bool IsGod;

    public bool KillEnemy;
    float time = 0;
    public Player player;
    [SerializeField] GameObject[] BackGroundPrefab;
    [SerializeField] GameObject GameEndPanel;
    [SerializeField] Text GameEndText;
    [SerializeField] Text GameEndScore;
    [SerializeField] GameObject InputField;
    [SerializeField] Text InputFieldText;
    [SerializeField] Text IsSaveText;
    [Header("슬라이더")]
    [SerializeField] Image XPSlider;
    [SerializeField] Image HPSlider, GasSlider;
    [SerializeField] Text XPText;
    [SerializeField] Text ScoreText;
    public Text StageText; 

    public Image FadePanel;
    void Start()
    {
        SaveLoad();
        StartCoroutine(FadeLoad());
        Instantiate(BackGroundPrefab[StageNum.StageNumber], new Vector3(0, 0, 0), Quaternion.identity);
        GameEndPanel.SetActive(false);
        if(StageNum.StageNumber == 0) StageNum.Score = 0;
        MaxHP = HP;
        MaxGas = Gas;
    }
    void SaveLoad()
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
    IEnumerator FadeLoad()
    {
        StageText.text = $"Stage {StageNum.StageNumber + 1}";
        var f = FadePanel.color.a;
        f = 1;
        while (f > 0)
        {
            f -= 0.05f;
            FadePanel.color = new Color(0, 0, 0, f);
            yield return new WaitForSeconds(0.025f);
        }
    }
    IEnumerator FadeOut(string LoadScene)
    {
        var f = FadePanel.color.a;
        f = 0;
        while (f < 1)
        {
            print(f);
            f += 0.05f;
            FadePanel.color = new Color(0, 0, 0, f);
            yield return new WaitForSeconds(0.025f);
        }
        SceneManager.LoadScene(LoadScene);

    }

    // Update is called once per frame
    void Update()
    {
        XPUI();
        HPUI();
        GasUI();
        ScoreUI();
        if (IsNextStage) isClear();
        if (KillEnemy) KillEnemy = false;
    }
    void HPUI()
    {
        float Hp = HP, MaxHp = MaxHP;
        HPSlider.fillAmount = Hp / MaxHp;
        if (HP <= 0 && !IsUI)
        {
            GameOver();
        }
    }
    void GasUI()
    {
        float gas = Gas, Maxgas = MaxGas;
        GasSlider.fillAmount = gas / MaxGas;
        if(Gas <= 0 && !IsUI)
        {
            GameOver();
        }
    }
    void ScoreUI()
    {
        ScoreText.text = $"Score : {StageNum.Score}";
    }
    void GameOver()
    {

        player.gameObject.SetActive(false);

        IsUI = true;
        GameEndPanel.SetActive(true);
        InputField.SetActive(false);
        IsSaveText.color = new Color(1, 1, 1, 0);
        GameEndText.text = "Game Over";
        GameEndScore.text = $"Score : {StageNum.Score}";
    }
    public void GameClear()
    {
        if (!IsUI)
        {
            IsUI = true;
            player.gameObject.SetActive(false);
            GameEndPanel.SetActive(true);
            IsSaveText.color = new Color(1, 1, 1, 0);
            GameEndText.text = "Game Clear!";
            GameEndScore.text = $"Score : {StageNum.Score}";
        }
    }
    void XPUI()
    {
        if (XP >= XPGauge[Level] && Level < 4)
        {
            XP -= XPGauge[Level];
            Level++;
        }
        else if (XP <= 0 && Level != 0)
        {
            Level--;
            XP += XPGauge[Level];
        }
        if (Level >= 4)
        {
            XPSlider.color = Color.cyan;
        }
        else
        {
            XPSlider.color = new Color(1, 0.5f, 0);
        }
        XP = Mathf.Clamp(XP, 0, XPGauge[Level]);
        XPSlider.fillAmount = XP / XPGauge[Level];
        XPText.text = $"Lv.{Level + 1}";
    }
    public void GameExit()
    {
        StageNum.StageNumber = 0;
        StartCoroutine(FadeOut("Title"));
    }
    public void SaveMethod()
    {
        if (InputField.activeSelf)
        {
            if (InputFieldText.text != "")
            {
                PlayerData play = new PlayerData();
                play.Name = InputFieldText.text;
                play.Score = StageNum.Score;
                foreach (var item in save.PlayerData)
                {
                    if (InputFieldText.text == item.Name)
                    {
                        save.PlayerData.Remove(item);
                        break;
                    }  
                }
                save.PlayerData.Add(play);
                PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(save));
                StartCoroutine(Saved());
                IEnumerator Saved()
                {
                    var f = IsSaveText.color.a;
                    f = 1;
                    IsSaveText.color = new Color(1, 1, 1, f);
                    yield return new WaitForSeconds(1);
                    while (f > 0)
                    {
                        f -= 0.05f;
                        IsSaveText.color = new Color(1, 1, 1, f);
                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
        }
        else
        {
            InputField.SetActive(true);
        }
    }
    public void isClear()
    {
        IsGod = true;
        time += Time.deltaTime;
        if (time >= 2)
        {
            print(player.transform.position.y);
            player.transform.Translate(Vector3.up * player.MoveSpeed * Time.deltaTime);
            if (player.transform.position.y >= 6)
            {
                print("start");
                time = 0;
                if (StageNum.StageNumber == 2)
                {
                    GameClear();
                    return;
                }
                if (!IsUI)
                {
                    StartCoroutine(FadeOut("Main"));
                }
            }
        }
    }
}
