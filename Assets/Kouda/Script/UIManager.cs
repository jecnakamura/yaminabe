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
    public void ShowBranchOptions(List<int> nextIndices)
    {
        branchPanel.SetActive(true);

        // �Â��{�^�����폜
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // �V�����{�^�����쐬
        foreach (int index in nextIndices)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"�}�X {index}";

            // �{�^�����N���b�N���ꂽ�Ƃ��̏���
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
