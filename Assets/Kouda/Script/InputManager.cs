using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Vector2 GetMoveInput()
    {
        // �X�e�B�b�N�܂��͏\���L�[�̓���
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }

    public bool IsConfirmPressed()
    {
        return Input.GetButtonDown("Submit"); // A�{�^���ɑΉ�
    }

    public bool IsCancelPressed()
    {
        return Input.GetButtonDown("Cancel"); // B�{�^���ɑΉ�
    }
}
