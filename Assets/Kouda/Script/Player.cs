using System.Collections.Generic;
using System.Linq;

public class Player
{
    public int ID;
    public Character chara;
    public List<Ingredient> ingredients; // �v���C���[���������Ă���H�ރ��X�g
    

    public Player()
    {
        ingredients = new List<Ingredient>();
    }

    // �v���C���[�̑����X�R�A���v�Z���郁�\�b�h
    public float CalculateScore()
    {
        // 1. ���ׂĂ̐H�ރX�R�A�����v
        int totalScore = ingredients.Sum(ingredient => ingredient.Score);

        // 2. ���ϑ����l���v�Z�i���ׂĂ̐H�ނ̑����l�̕��ςƂ���j
        float averageCompatibility = ingredients.Average(ingredient => ingredient.Compatibility);

        // 3. ���X�R�A�𑊐��l�Œ���
        return totalScore * averageCompatibility;
    }
}
