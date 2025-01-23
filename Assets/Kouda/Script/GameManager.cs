using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // プレイヤーリストを保持
    public List<Player> players;

    private void Awake()
    {
        // シーンが切り替わってもこのオブジェクトを破棄しないようにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // このオブジェクトはシーン遷移後も残る
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // プレイヤーデータをセットするメソッド
    public void SetPlayers(List<Player> playersList)
    {
        players = playersList;
    }

    // プレイヤーデータを取得するメソッド
    public List<Player> GetPlayers()
    {
        return players;
    }
}
