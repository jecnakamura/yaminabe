using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;

public class RouletteController : MonoBehaviour
{
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;
    private string result;
    private int id;
    private float rouletteSpeed;
    private float slowDownSpeed;
    private int frameCount;
    private bool isPlaying;
    private bool isStop;
    [SerializeField] private Text resultText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    // [SerializeField] private Button retryButton;

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    //別コードから渡されるプレイヤー情報
    Player player;

    void Start()
    {
        csvFile = Resources.Load("syokuzai") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }
        public void SetRoulette()
    {
        isPlaying = false;
        isStop = false;
        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
       // retryButton.gameObject.SetActive(false);
        startButton.onClick.AddListener(StartOnClick);
        stopButton.onClick.AddListener(StopOnClick);
       // retryButton.onClick.AddListener(RetryOnClick);
    }

    private void Update()
    {
        if (!isPlaying) return;
        roulette.transform.Rotate(0, 0, rouletteSpeed);
        frameCount++;
        if (isStop && frameCount > 3)
        {
            rouletteSpeed *= slowDownSpeed;
            slowDownSpeed -= 0.25f * Time.deltaTime;
            frameCount = 0;
        }
        if (rouletteSpeed < 0.05f)
        {
            isPlaying = false;
            ShowResult(roulette.transform.eulerAngles.z);
        }
    }

    private void StartOnClick()
    {
        rouletteSpeed = 14f;
        startButton.gameObject.SetActive(false);
        Invoke("ShowStopButton", 1.5f);
        isPlaying = true;
    }

    private void StopOnClick()
    {
        slowDownSpeed = Random.Range(0.92f, 0.98f);
        isStop = true;
        stopButton.gameObject.SetActive(false);
    }

    //private void RetryOnClick()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    private void ShowStopButton()
    {
        stopButton.gameObject.SetActive(true);
    }

    private void ShowResult(float x)
    {

        for (int i = 1; i <= rMaker.choices.Count; i++)
        {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette))))
            {
                result = rMaker.choices[i - 1];
                id = rMaker.ID[i - 1];
            }
        }
        resultText.text = result + "\nが当たったよ！";
        Debug.Log("ID：" + csvDatas[id + 1][0] + ", 名前：" + csvDatas[id + 1][1] + ", ジャンル：" + csvDatas[id + 1][2] + ", スコア：" + csvDatas[id + 1][3]);
        //  retryButton.gameObject.SetActive(true);
    }
    public void PlayerResult(Player player)
    {
        player.AddIngredient(new Ingredient(id, csvDatas[id + 1][1], csvDatas[id + 1][2], int.Parse(csvDatas[id + 1][3])));
    }
}