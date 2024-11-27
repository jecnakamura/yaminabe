using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int ID;                              // プレイヤーID
    public Vector3 CurrentPosition;            // 現在位置
    public List<Ingredient> Ingredients { get; private set; } // 所持食材
    public bool HasKey { get; set; }           // 鍵の所持状態
    public bool HasFinished { get; set; }      // ゴール状態
    public int MoveSteps { get; set; }         // 移動するマス数

    public Player()
    {
        Ingredients = new List<Ingredient>();
        HasKey = false;
        HasFinished = false;
        MoveSteps = 0;
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

}
