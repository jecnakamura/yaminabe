using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI turnInfoText; // �^�[�����e�L�X�g
    public GameObject branchPanel; // ����I��p�p�l��
    public GameObject branchButtonPrefab; // ����{�^���̃v���n�u
    public Transform branchButtonContainer; // �{�^����z�u����e�I�u�W�F�N�g

    public delegate void BranchSelectedHandler(int selectedIndex);
    public event BranchSelectedHandler OnBranchSelected;

    // �^�[������\��
    public void ShowTurnInfo(Player player)
    {
        turnInfoText.text = $"�v���C���[{player.ID + 1} �̃^�[��";
    }

    // �Q�[���I�����UI�X�V
    public void ShowEndGameInfo()
    {
        turnInfoText.text = "�Q�[���I���I";
    }

    // ����I�v�V������\��
    public void ShowBranchOptions(List<int> nextIndices, Player player)
    {
        branchPanel.SetActive(true);

        // �Â��{�^�����폜
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // �{�^����z�u����Y���W�̏����l
        float[] yPositions = new float[] { 450f, 0f, -450f };
        string[] labels = new string[] { "��", "�^��", "��" }; // ��A�^�񒆁A���̃��x��

        // �V�����{�^�����쐬
        for (int i = 0; i < nextIndices.Count; i++)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();

            // �C���f�b�N�X�ɉ����ă��x����ݒ�
            button.GetComponentInChildren<TextMeshProUGUI>().text = labels[i];

            // �{�^���̈ʒu���� (X���W��375�AY���W��yPositions�ŕύX)
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = new Vector2(375f, yPositions[i]);  // X = 375, Y��yPositions[i]

            // �{�^�����N���b�N���ꂽ�Ƃ��̏���
            int index = nextIndices[i];  // �֐����̃C���f�b�N�X���g�����߂Ƀ��[�J���ϐ��Ɋi�[
            button.onClick.AddListener(() =>
            {
                OnBranchSelected?.Invoke(index);
                HideBranchOptions();
            });
        }
    }

    // ����I�v�V�������\��
    public void HideBranchOptions()
    {
        branchPanel.SetActive(false);
    }
}
