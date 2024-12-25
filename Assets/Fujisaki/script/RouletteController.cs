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
    public int id;
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
    Player player=new Player();

    void Start()
    {
        csvFile = Resources.Load("syokuzai") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            if (!string.IsNullOrWhiteSpace(line)) // 空行を無視
            {
                csvDatas.Add(line.Split(','));
            }
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
          //  PlayerResult(player);
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
                Debug.Log($"選択肢: {rMaker.choices}, ID: {rMaker.ID}");
                if (i - 1 < rMaker.choices.Count && i - 1 < rMaker.ID.Count)
                {
                    result = rMaker.choices[i - 1];
                    id = rMaker.ID[i - 1];
                }
                else
                {
                    Debug.LogError($"ルーレット結果が範囲外です。インデックス: {i - 1}");
                }
            }
        }
        resultText.text = result + "\nが当たったよ！";
        Debug.Log($"ルーレット結果判定: x={x}, rotatePerRoulette={rotatePerRoulette}");
        Debug.Log($"rMaker.choices.Count={rMaker.choices.Count}, rMaker.ID.Count={rMaker.ID.Count}");
        Debug.Log($"ID={id}, csvDatas.Count={csvDatas.Count}");
        //Debug.Log("ID：" + csvDatas[id][0] + ", 名前：" + csvDatas[id][1] + ", ジャンル：" + csvDatas[id][2] + ", スコア：" + csvDatas[id][3]);
        //  retryButton.gameObject.SetActive(true);

       
        if (id >= 0 && id < csvDatas.Count && csvDatas[id].Length >= 4)
        {
            Ingredient newIngredient = new Ingredient(
               id,
                csvDatas[id][1],
                csvDatas[id][2],
                int.Parse(csvDatas[id][3])
            );
            player.AddIngredient(newIngredient);
            Debug.Log($"プレイヤーにアイテムを追加: ID={id}, 名前={newIngredient.Name}, ジャンル={newIngredient.Type}, スコア={newIngredient.Score}");
        }
        else
        {
            Debug.LogError($"データが不足しています。ID: {id}, CSVデータ数: {csvDatas.Count}");
        }

     
    }
    //public void PlayerResult(Player player)
    //{
    //    player.AddIngredient(new Ingredient(id, csvDatas[id][1], csvDatas[id][2], int.Parse(csvDatas[id][3])));
    //   // Debug.Log(player.ingredients[]);
    //}
}