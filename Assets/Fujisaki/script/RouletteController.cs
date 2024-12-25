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

    TextAsset csvFile; // CSV�t�@�C��
    List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;

    //�ʃR�[�h����n�����v���C���[���
    Player player=new Player();

    void Start()
    {
        csvFile = Resources.Load("syokuzai") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            if (!string.IsNullOrWhiteSpace(line)) // ��s�𖳎�
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
                Debug.Log($"�I����: {rMaker.choices}, ID: {rMaker.ID}");
                if (i - 1 < rMaker.choices.Count && i - 1 < rMaker.ID.Count)
                {
                    result = rMaker.choices[i - 1];
                    id = rMaker.ID[i - 1];
                }
                else
                {
                    Debug.LogError($"���[���b�g���ʂ��͈͊O�ł��B�C���f�b�N�X: {i - 1}");
                }
            }
        }
        resultText.text = result + "\n������������I";
        Debug.Log($"���[���b�g���ʔ���: x={x}, rotatePerRoulette={rotatePerRoulette}");
        Debug.Log($"rMaker.choices.Count={rMaker.choices.Count}, rMaker.ID.Count={rMaker.ID.Count}");
        Debug.Log($"ID={id}, csvDatas.Count={csvDatas.Count}");
        //Debug.Log("ID�F" + csvDatas[id][0] + ", ���O�F" + csvDatas[id][1] + ", �W�������F" + csvDatas[id][2] + ", �X�R�A�F" + csvDatas[id][3]);
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
            Debug.Log($"�v���C���[�ɃA�C�e����ǉ�: ID={id}, ���O={newIngredient.Name}, �W������={newIngredient.Type}, �X�R�A={newIngredient.Score}");
        }
        else
        {
            Debug.LogError($"�f�[�^���s�����Ă��܂��BID: {id}, CSV�f�[�^��: {csvDatas.Count}");
        }

     
    }
    //public void PlayerResult(Player player)
    //{
    //    player.AddIngredient(new Ingredient(id, csvDatas[id][1], csvDatas[id][2], int.Parse(csvDatas[id][3])));
    //   // Debug.Log(player.ingredients[]);
    //}
}