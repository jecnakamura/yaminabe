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
        // �v���C���[�̏�Ԃ⑼�̃v���C���[�Ƃ̃C�x���g������
        if (tileType == TileType.Normal)
        {
           // Ingredient newIngredient = GenerateIngredient();
            //player.AddIngredient(newIngredient);
        }
        else if (tileType == TileType.Event)
        {
            // �C�x���g���e������
            player.RemoveRandomIngredient();
        }
        else if (tileType == TileType.Goal)
        {
            player.HasFinished = true;
        }
    }

    //private Ingredient GenerateIngredient()
    //{
    //    //// �����_���ȐH�ނ𐶐�
    //    //string[] names = { "����", "�l�M", "����" };
    //    //string[] types = { "���", "��", "����" };
    //    //string name = names[Random.Range(0, names.Length)];
    //    //string type = types[Random.Range(0, types.Length)];
    //    //int score = Random.Range(10, 100);
    //    //float compatibility = Random.Range(0.5f, 1.5f);

    //    //return new Ingredient(name, type, score, compatibility);
    //}
}
