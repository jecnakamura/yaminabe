using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int ID;                              // �v���C���[ID
    public Vector3 CurrentPosition;            // ���݈ʒu
    public List<Ingredient> Ingredients { get; private set; } // �����H��
    public bool HasKey { get; set; }           // ���̏������
    public bool HasFinished { get; set; }      // �S�[�����
    public int MoveSteps { get; set; }         // �ړ�����}�X��

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
        return Ingredients.Sum(ingredient => ingredient.Score); // ���ׂĂ̐H�ނ̃X�R�A�����v
    }

}
