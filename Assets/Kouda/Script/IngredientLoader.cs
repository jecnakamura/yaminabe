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
            Debug.LogError("CSV�t�@�C����������܂���: " + filePath);
            return ingredientList;
        }

        // ���s�ŕ������Ċe�s������
        string[] lines = data.text.Split('\n');

        // 1�s�ڂ̓w�b�_�̂��߃X�L�b�v
        for (int i = 1; i < lines.Length; i++)
        {
            // �e�s�̗v�f���J���}�ŕ���
            string[] fields = lines[i].Split(',');

            // �ǂݍ��񂾍s����V����Ingredient�𐶐�
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
