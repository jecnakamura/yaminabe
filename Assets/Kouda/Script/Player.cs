using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int ID;                              // �v���C���[ID
    public Vector3Int CurrentPosition;            // ���݈ʒu
    public List<Ingredient> Ingredients { get; set; } // �����H��
    public bool HasKey { get; set; }           // ���̏������
    public bool HasFinished { get; set; }      // �S�[�����
    public int MoveSteps { get; set; }         // �ړ�����}�X��

    public Camera camera;                       // �v���C���[�^�[�����ɌX���ʂ��J����
    public Character chara;
    public List<Ingredient> ingredients;

    public SpriteRenderer display;
    public int nowIndex = 0;

    private int controllerIndex; // ���蓖�Ă�ꂽ�R���g���[���[�ԍ�

    public List<string> RouletteHistory { get; set; } //�񂵂����[���b�g�̗���

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
        // ���̃I�u�W�F�N�g�� SpriteRenderer ���擾
        display = GetComponent<SpriteRenderer>();

        // GameData ����R���g���[���[�ԍ����擾
        //controllerIndex = GameData.controllerAssignments[ID];
    }

    void Update()
    {
        // ���蓖�Ă�ꂽ�R���g���[���[�̂ݓ��͂��󂯕t����
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
        //    // �v���C���[�̈ړ����W�b�N��ǉ�
        //}

        //if (Input.GetButtonDown($"Joystick{controllerIndex + 1}ButtonA"))
        //{
        //    Debug.Log($"Player {ID} pressed A button");
        //    // ���̃A�N�V������ǉ�
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
        return Ingredients.Sum(ingredient => ingredient.Score); // ���ׂĂ̐H�ނ̃X�R�A�����v
    }

    public void SetCharaImage()
    {
        display.sprite = chara.image;
    }
    // ���[���b�g�̌��ʂ�ǉ�
    public void AddRouletteResult(string rouletteType)
    {
        RouletteHistory.Add(rouletteType);
    }

    // ���[���b�g�����̃J�E���g
    public string GetMostFrequentRoulette()
    {
        var frequency = RouletteHistory.GroupBy(x => x)
                                       .OrderByDescending(g => g.Count())
                                       .FirstOrDefault();

        return frequency?.Key ?? "None";  // �ł������񂳂ꂽ���[���b�g�̎��
    }
}
