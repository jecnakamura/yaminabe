using System.Collections.Generic;
using UnityEngine;

public class IngredientLoader : MonoBehaviour
{
    public List<Ingredient> ingredients;

    private void Start()
    {
        ingredients = LoadIngredients("Data/Ingredients");
    }

    private List<Ingredient> LoadIngredients(string filePath)
    {
        List<Ingredient> ingredientList = new List<Ingredient>();
        TextAsset data = Resources.Load<TextAsset>(filePath);

        if (data == null)
        {
            Debug.LogError("CSVファイルが見つかりません: " + filePath);
            return ingredientList;
        }

        // 改行で分割して各行を処理
        string[] lines = data.text.Split('\n');

        // 1行目はヘッダのためスキップ
        for (int i = 1; i < lines.Length; i++)
        {
            // 各行の要素をカンマで分割
            string[] fields = lines[i].Split(',');

            // 読み込んだ行から新しいIngredientを生成
            if (fields.Length >= 4)
            {
                string name = fields[0].Trim();
                string type = fields[1].Trim();
                int score = int.Parse(fields[2].Trim());
                float compatibility = float.Parse(fields[3].Trim());

                Ingredient ingredient = new Ingredient(name, type, score, compatibility);
                ingredientList.Add(ingredient);
            }
        }

        return ingredientList;
    }
}
