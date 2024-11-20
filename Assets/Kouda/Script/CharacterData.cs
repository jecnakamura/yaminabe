using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
}
