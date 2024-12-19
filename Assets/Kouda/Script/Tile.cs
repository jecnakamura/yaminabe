using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Normal,
        Event,
        Goal
    }

    public TileType tileType;
    public string ingredientType;

    public void ExecuteEvent(Player player, List<Player> allPlayers)
    {
        // プレイヤーの状態や他のプレイヤーとのイベントを処理
        if (tileType == TileType.Normal)
        {
           // Ingredient newIngredient = GenerateIngredient();
            //player.AddIngredient(newIngredient);
        }
        else if (tileType == TileType.Event)
        {
            // イベント内容を実装
            player.RemoveRandomIngredient();
        }
        else if (tileType == TileType.Goal)
        {
            player.HasFinished = true;
        }
    }

    //private Ingredient GenerateIngredient()
    //{
    //    //// ランダムな食材を生成
    //    //string[] names = { "白菜", "ネギ", "豆腐" };
    //    //string[] types = { "野菜", "肉", "魚介" };
    //    //string name = names[Random.Range(0, names.Length)];
    //    //string type = types[Random.Range(0, types.Length)];
    //    //int score = Random.Range(10, 100);
    //    //float compatibility = Random.Range(0.5f, 1.5f);

    //    //return new Ingredient(name, type, score, compatibility);
    //}
}
