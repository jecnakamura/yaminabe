using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    private int selectedPlayerCount = 0;     // �I�����ꂽ�v���C���[�l��
    private int maxPlayers = 4;              // �ő�v���C���[���i�Œ�j
    public GameObject[] playerCountButtons; // 1�`4�l�I���̃{�^��
    public GameObject backButton; // �߂�{�^��
    private int currentIndex = 0;
    public void Start()
    {
        GameData.playerCount = 0;
        UpdateButtonSelection();
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        // �\���L�[�܂��̓X�e�B�b�N�őI�����ړ�
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            MoveSelection(-1);
        }

        // A�{�^���Ō���
        if (Input.GetButtonDown("Submit"))
        {
            if (currentIndex < playerCountButtons.Length)
            {
                playerCountButtons[currentIndex].GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
            else
            {
                backButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }
        }

        // B�{�^���Ń^�C�g����ʂɖ߂�
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void MoveSelection(int direction)
    {
        int buttonCount = playerCountButtons.Length + 1; // �߂�{�^�����܂�
        currentIndex = (currentIndex + direction + buttonCount) % buttonCount;
        UpdateButtonSelection();
    }

    private void UpdateButtonSelection()
    {
        if (currentIndex < playerCountButtons.Length)
        {
            EventSystem.current.SetSelectedGameObject(playerCountButtons[currentIndex]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(backButton);
        }
    }
    // �v���C���[�l�����{�^���őI�����郁�\�b�h
    public void SelectPlayerCount(int playerCount)
    {
        if (playerCount > 0 && playerCount <= maxPlayers)
        {
            selectedPlayerCount = playerCount;
            Debug.Log($"�I�����ꂽ�v���C���[�l��: {selectedPlayerCount}");
            StartGame();
        }
        else
        {
            Debug.LogWarning("�����ȃv���C���[�l�����I������܂���");
        }
    }

    // �Q�[���J�n�{�^��
    public void StartGame()
    {
        if (selectedPlayerCount > 0)
        {
            GameData.playerCount = selectedPlayerCount;  // �I�����ꂽ�l����ۑ�
            SceneManager.LoadScene("CharacterSelectionScene");  // �L�����N�^�[�I���V�[���֑J��
        }
        else
        {
            Debug.LogWarning("�v���C���[�l����I�����Ă�������");
        }
    }

    public void GoToRules()
    {
        SceneManager.UnloadSceneAsync("TitleScene");
    }
}
