using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int playerCount = 0; // �v���C���[�l��
    public static Character[] selectedCharacters = new Character[4]; // �v���C���[�I���L�����N�^�[
    public static List<NPCData> npcData = new List<NPCData>(); // NPC�f�[�^���X�g
}

[System.Serializable]
public class NPCData
{
    public Character assignedCharacter; // NPC�Ɋ��蓖�Ă�ꂽ�L�����N�^�[
    public NPCStrength npcStrength;    // NPC�̋���
}
