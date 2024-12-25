using System.Collections.Generic;

public static class GameData
{
    public static int playerCount = 0; // プレイヤー人数
    public static Character[] selectedCharacters = new Character[4]; // プレイヤー選択キャラクター
    public static List<NPCData> npcData = new List<NPCData>(); // NPCデータリスト
    public static int[] controllerAssignments = new int[4]; // プレイヤーごとのコントローラー番号
}

[System.Serializable]
public class NPCData
{
    public Character assignedCharacter; // NPCに割り当てられたキャラクター
    public NPCStrength npcStrength;    // NPCの強さ
}
