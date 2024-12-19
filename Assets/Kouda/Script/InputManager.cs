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
        // スティックまたは十字キーの入力
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }

    public bool IsConfirmPressed()
    {
        return Input.GetButtonDown("Submit"); // Aボタンに対応
    }

    public bool IsCancelPressed()
    {
        return Input.GetButtonDown("Cancel"); // Bボタンに対応
    }
}
