using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static List<Character> selectedCharacters = new List<Character>(); // �I�����ꂽ�L�����N�^�[���X�g
    public static int playerCount = 0; // �v���C���[�l��

    // NPC�̃��X�g�B�Q�[���J�n���ɐݒ肳�ꂽ�������܂�
    public static List<NPC> npcs = new List<NPC>();

}
