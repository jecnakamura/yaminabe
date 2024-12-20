using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite image;
    public bool HasKey { get; set; } // 鍵を持っているかどうか
    public bool HasFinished { get; set; } // ゴールしているかどうか
}
