using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();
    public Dictionary<string, Dictionary<string, float>> Compatibility { get; private set; } = new Dictionary<string, Dictionary<string, float>>();

    private string ingredientsFilePath = "syokuzai.csv";
    private string compatibilityFilePath = "aisyou.csv";

    void Awake()
    {
        LoadIngredients();
        LoadCompatibility();
    }

    private void LoadIngredients()
    {
        string path = Path.Combine(Application.streamingAssetsPath, ingredientsFilePath);
        if (!File.Exists(path))
        {
            Debug.LogError($"�H�ރf�[�^�t�@�C����������܂���: {path}");
            return;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("ID")) continue; // �w�b�_�[���X�L�b�v

                string[] data = line.Split(',');
                if (data.Length != 4) continue;

                int id = int.Parse(data[0]);
                string name = data[1];
                string genre = data[2];
                int score = int.Parse(data[3]);

                //Ingredients.Add(new Ingredient(id, name, genre, score));
            }
        }

        Debug.Log($"�H�ރf�[�^�����[�h���܂���: {Ingredients.Count}��");
    }

    private void LoadCompatibility()
    {
        string path = Path.Combine(Application.streamingAssetsPath, compatibilityFilePath);
        if (!File.Exists(path))
        {
            Debug.LogError($"�W�����������f�[�^�t�@�C����������܂���: {path}");
            return;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            string headerLine = reader.ReadLine(); // �w�b�_�[�s
            if (headerLine == null) return;

            string[] genres = headerLine.Split(',');
            for (int i = 1; i < genres.Length; i++)
            {
                Compatibility[genres[i]] = new Dictionary<string, float>();
            }

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                string baseGenre = data[0];
                for (int i = 1; i < data.Length; i++)
                {
                    float value = float.Parse(data[i]);
                    Compatibility[baseGenre][genres[i]] = value;
                }
            }
        }

        Debug.Log("�W�����������f�[�^�����[�h���܂���");
    }

    public float GetCompatibility(string baseGenre, string targetGenre)
    {
        if (Compatibility.ContainsKey(baseGenre) && Compatibility[baseGenre].ContainsKey(targetGenre))
        {
            return Compatibility[baseGenre][targetGenre];
        }

        Debug.LogWarning($"�����f�[�^��������܂���: {baseGenre} -> {targetGenre}");
        return 1f; // �f�t�H���g�l
    }
}
