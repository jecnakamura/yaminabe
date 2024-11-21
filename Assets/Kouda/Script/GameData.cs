using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int playerCount = 0; // プレイヤー人数
    public static Character[] selectedCharacters = new Character[4]; // プレイヤー選択キャラクター
    public static List<NPCData> npcData = new List<NPCData>(); // NPCデータリスト
}

[System.Serializable]
public class NPCData
{
    public Character assignedCharacter; // NPCに割り当てられたキャラクター
    public NPCStrength npcStrength;    // NPCの強さ
}
