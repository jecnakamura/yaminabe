using UnityEngine;

public class FoodRoulette : MonoBehaviour
{
    private System.Action<int> onRouletteFinished;

    // ルーレットを開始する
    public void StartRoulette(System.Action<int> callback)
    {
        onRouletteFinished = callback;
        Debug.Log("ルーレットを回します");

        // 実際のルーレット処理をここに実装
        int result = Random.Range(1, 7); // 仮の結果: 1〜6マス進む
        FinishRoulette(result);
    }

    // ルーレット終了時の処理
    private void FinishRoulette(int result)
    {
        Debug.Log($"ルーレットの結果: {result}");
        onRouletteFinished?.Invoke(result);
    }
}
