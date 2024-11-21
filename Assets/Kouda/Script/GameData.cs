using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static List<Character> selectedCharacters = new List<Character>(); // 選択されたキャラクターリスト
    public static int playerCount = 0; // プレイヤー人数

    // NPCのリスト。ゲーム開始時に設定された強さも含む
    public static List<NPC> npcs = new List<NPC>();

}
