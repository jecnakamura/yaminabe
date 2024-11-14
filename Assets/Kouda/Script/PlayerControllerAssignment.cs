using UnityEngine;

public class PlayerControllerAssignment : MonoBehaviour
{
    // プレイヤー数の最大値
    private int maxPlayers = 4;

    // コントローラーの接続状況を保持するリスト
    private bool[] controllersConnected;

    void Start()
    {
        // コントローラーの接続状況を管理する配列を初期化
        controllersConnected = new bool[maxPlayers];

        // 接続されているコントローラーの数を取得
        CheckControllers();
    }

    void Update()
    {
        // コントローラーが接続された場合、接続の変更を検知
        CheckControllers();
    }

    void CheckControllers()
    {
        // 接続されているコントローラーの数を検出
        string[] joystickNames = Input.GetJoystickNames();

        // すべてのコントローラーを確認
        for (int i = 0; i < maxPlayers; i++)
        {
            // コントローラーが接続されている場合
            if (i < joystickNames.Length && !string.IsNullOrEmpty(joystickNames[i]))
            {
                if (!controllersConnected[i])
                {
                    // 新たにコントローラーが接続された場合、プレイヤー番号を割り当て
                    controllersConnected[i] = true;
                    Debug.Log($"コントローラー{i + 1}が接続されました。プレイヤー{i + 1}に割り当てました。");
                }
            }
            else
            {
                // コントローラーが切断された場合、接続状態をリセット
                if (controllersConnected[i])
                {
                    controllersConnected[i] = false;
                    Debug.Log($"コントローラー{i + 1}が切断されました。");
                }
            }
        }
    }
}
