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
    public void ShowBranchOptions(List<int> nextIndices,Player player)
    {
        branchPanel.SetActive(true);

        // �Â��{�^�����폜
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // �����ʒu�iY���j�̐ݒ�
        float buttonHeight = 50f;  // �e�{�^���̍����i���̒l�j
        float offsetY = 0f;

        // �V�����{�^�����쐬
        foreach (int index in nextIndices)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"�}�X {index}";

            // �{�^���̈ʒu�����炷
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = new Vector2(0, offsetY);

            // �{�^�����N���b�N���ꂽ�Ƃ��̏���
            button.onClick.AddListener(() =>
            {
                OnBranchSelected?.Invoke(index);
                HideBranchOptions();
            });

            // ���̃{�^���̈ʒu�����炷
            offsetY -= buttonHeight;  // �{�^�������ɔz�u���邽�߂�Y�������ɃI�t�Z�b�g
        }
    }

    // ����I�v�V�������\��
    public void HideBranchOptions()
    {
        branchPanel.SetActive(false);
    }
}
