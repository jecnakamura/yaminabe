using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int ID;                              // プレイヤーID
    public Vector3Int CurrentPosition;            // 現在位置
    public List<Ingredient> Ingredients { get; set; } // 所持食材
    public bool HasKey { get; set; }           // 鍵の所持状態
    public bool HasFinished { get; set; }      // ゴール状態
    public int MoveSteps { get; set; }         // 移動するマス数

    public Camera camera;                       // プレイヤーターン時に個々を写すカメラ
    public Character chara;
    public List<Ingredient> ingredients;

    public SpriteRenderer display;
    public int nowIndex = 0;

    private int controllerIndex; // 割り当てられたコントローラー番号

    public List<string> RouletteHistory { get; set; } //回したルーレットの履歴

    public Player()
    {
        Ingredients = new List<Ingredient>();
        RouletteHistory = new List<string>();
        HasKey = false;
        HasFinished = false;
        MoveSteps = 0;
    }

    void Awake()
    {
        // このオブジェクトの SpriteRenderer を取得
        display = GetComponent<SpriteRenderer>();

        // GameData からコントローラー番号を取得
        //controllerIndex = GameData.controllerAssignments[ID];
    }

    void Update()
    {
        // 割り当てられたコントローラーのみ入力を受け付ける
        //if (controllerIndex != -1)
        //{
        //    HandleInput();
        //}
    }

    void HandleInput()
    {
        //float horizontal = Input.GetAxis($"Joystick{controllerIndex + 1}AxisX");
        //float vertical = Input.GetAxis($"Joystick{controllerIndex + 1}AxisY");

        //if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        //{
        //    Debug.Log($"Player {ID} is moving: ({horizontal}, {vertical})");
        //    // プレイヤーの移動ロジックを追加
        //}

        //if (Input.GetButtonDown($"Joystick{controllerIndex + 1}ButtonA"))
        //{
        //    Debug.Log($"Player {ID} pressed A button");
        //    // 他のアクションを追加
        //}
    }

    public void AddIngredient(Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
    }

    public void RemoveRandomIngredient()
    {
        if (Ingredients.Count > 0)
        {
            int randomIndex = Random.Range(0, Ingredients.Count);
            Ingredients.RemoveAt(randomIndex);
        }
    }

    public int CalculateScore()
    {
        return Ingredients.Sum(ingredient => ingredient.Score); // すべての食材のスコアを合計
    }

    public void SetCharaImage()
    {
        display.sprite = chara.image;
    }
    // ルーレットの結果を追加
    public void AddRouletteResult(string rouletteType)
    {
        RouletteHistory.Add(rouletteType);
    }

    // ルーレット履歴のカウント
    public string GetMostFrequentRoulette()
    {
        var frequency = RouletteHistory.GroupBy(x => x)
                                       .OrderByDescending(g => g.Count())
                                       .FirstOrDefault();

        return frequency?.Key ?? "None";  // 最も多く回されたルーレットの種類
    }
}
