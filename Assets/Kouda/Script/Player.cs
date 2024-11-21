using System.Collections.Generic;
using System.Linq;

public class Player
{
    public int ID;
    public Character chara;
    public List<Ingredient> ingredients; // プレイヤーが所持している食材リスト
    

    public Player()
    {
        ingredients = new List<Ingredient>();
    }

    // プレイヤーの総合スコアを計算するメソッド
    public float CalculateScore()
    {
        // 1. すべての食材スコアを合計
        int totalScore = ingredients.Sum(ingredient => ingredient.Score);

        // 2. 平均相性値を計算（すべての食材の相性値の平均とする）
        float averageCompatibility = ingredients.Average(ingredient => ingredient.Compatibility);

        // 3. 総スコアを相性値で調整
        return totalScore * averageCompatibility;
    }
}
