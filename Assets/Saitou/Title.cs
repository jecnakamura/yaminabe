using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] menuButtons; // �{�^���I�u�W�F�N�g���C���X�y�N�^�[�Őݒ�
    private int currentIndex = 0;


    private void Start()
    {
        // ������Ԃōŏ��̃{�^����I��
        UpdateButtonSelection();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // �\���L�[�܂��̓X�e�B�b�N�őI�����ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            MoveSelection(-1);
        }

        // A�{�^���Ō���
        if (Input.GetButtonDown("Submit"))
        {
            menuButtons[currentIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }

    private void MoveSelection(int direction)
    {
        currentIndex = (currentIndex + direction + menuButtons.Length) % menuButtons.Length;
        UpdateButtonSelection();
    }
    private void UpdateButtonSelection()
    {
        // ���ݑI�𒆂̃{�^���������\��
        EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex]);
    }
    public void OnClick()
    {
        // TitleScene�����Ƀ��[�h����Ă��邩���m�F
        Scene titleScene = SceneManager.GetSceneByName("TitleScene");
        if (titleScene.isLoaded)
        {
            // ����TitleScene�����[�h����Ă���ꍇ�A�������Ȃ�
            return;
        }

        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
    }
    //public void OnClickRules()
    //{
    //    SceneManager.LoadScene("RuleScene");
    //}
}
