using UnityEngine;

public enum NPCStrength
{
    Unset,  // �ǉ�: ������Ԃ̐ݒ�
    Weak,
    Normal,
    Strong
}

public class NPC : MonoBehaviour
{
    public NPCStrength npcStrength = NPCStrength.Unset;  // ������Ԃ�Unset��
    public int playerIndex;           // NPC�̃C���f�b�N�X�i���Ԗڂ�NPC���j
    public Character assignedCharacter; // NPC�Ɋ��蓖�Ă�ꂽ�L�����N�^�[

    // ���[�g�I���̏���
    public void ChooseRoute()
    {
        switch (npcStrength)
        {
            case NPCStrength.Weak:
                ChooseRandomRoute();
                break;
            case NPCStrength.Normal:
                ChooseBasedOnIngredients();
                break;
            case NPCStrength.Strong:
                ChooseBasedOnIngredientsWithKeyRoute();
                break;
        }
    }

    // �アNPC�̃��[�g�I���i���S�����_���j
    private void ChooseRandomRoute()
    {
        Debug.Log($"NPC {playerIndex} is choosing a random route.");
        // �����_���ȃ��[�g�I������������
    }

    // ���ʂ�NPC�̃��[�g�I���i�H�ނɊ�Â��đI���j
    private void ChooseBasedOnIngredients()
    {
        Debug.Log($"NPC {playerIndex} is choosing a route based on ingredients.");
        // �H�ނɊ�Â������[�g�I�������������i�����[�g�͑I�΂Ȃ��j
    }

    // ����NPC�̃��[�g�I���i�H�ނɊ�Â��đI�����A�����[�g���g���j
    private void ChooseBasedOnIngredientsWithKeyRoute()
    {
        Debug.Log($"NPC {playerIndex} is choosing a route with keys.");
        // �H�ނɊ�Â������[�g�I�������i�����[�g���܂ށj
    }

    // �~�j�Q�[���ł̋���
    public void PlayMiniGame()
    {
        switch (npcStrength)
        {
            case NPCStrength.Weak:
                HandleWeakMiniGame();
                break;
            case NPCStrength.Normal:
                HandleNormalMiniGame();
                break;
            case NPCStrength.Strong:
                HandleStrongMiniGame();
                break;
        }
    }

    // �アNPC�̃~�j�Q�[��
    private void HandleWeakMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is very weak in mini-game.");
        // �~�j�Q�[���ł̎アNPC�̋����i�����ɓ|�����j
    }

    // ���ʂ�NPC�̃~�j�Q�[��
    private void HandleNormalMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is moderately strong in mini-game.");
        // �~�j�Q�[���ł̕��ʂ�NPC�̋����i�|���₷���j
    }

    // ����NPC�̃~�j�Q�[��
    private void HandleStrongMiniGame()
    {
        Debug.Log($"NPC {playerIndex} is a strong opponent in mini-game.");
        // �~�j�Q�[���ł̋���NPC�̋����i���G�j
    }
}
