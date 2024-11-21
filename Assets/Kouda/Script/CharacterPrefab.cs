using UnityEngine;

public class CharacterPrefab : MonoBehaviour
{
    public Character characterData; // Character ScriptableObject を参照

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // コンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // データを適用
        if (characterData != null)
        {
            spriteRenderer.sprite = characterData.image;
            gameObject.name = characterData.characterName; // GameObject にキャラ名を適用
        }
        else
        {
            Debug.LogWarning("Character Data が設定されていません: " + gameObject.name);
        }
    }
}
