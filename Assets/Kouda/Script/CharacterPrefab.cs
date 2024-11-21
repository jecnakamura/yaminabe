using UnityEngine;

public class CharacterPrefab : MonoBehaviour
{
    public Character characterData; // Character ScriptableObject ���Q��

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // �R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // �f�[�^��K�p
        if (characterData != null)
        {
            spriteRenderer.sprite = characterData.image;
            gameObject.name = characterData.characterName; // GameObject �ɃL��������K�p
        }
        else
        {
            Debug.LogWarning("Character Data ���ݒ肳��Ă��܂���: " + gameObject.name);
        }
    }
}
