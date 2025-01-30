using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI turnInfoText; // ターン情報テキスト
    public GameObject branchPanel; // 分岐選択用パネル
    public GameObject branchButtonPrefab; // 分岐ボタンのプレハブ
    public Transform branchButtonContainer; // ボタンを配置する親オブジェクト

    public delegate void BranchSelectedHandler(int selectedIndex);
    public event BranchSelectedHandler OnBranchSelected;

    // ターン情報を表示
    public void ShowTurnInfo(Player player)
    {
        turnInfoText.text = $"プレイヤー{player.ID + 1} のターン";
    }

    // ゲーム終了後のUI更新
    public void ShowEndGameInfo()
    {
        turnInfoText.text = "ゲーム終了！";
    }

    // 分岐オプションを表示
    public void ShowBranchOptions(List<int> nextIndices, Player player)
    {
        branchPanel.SetActive(true);

        // 古いボタンを削除
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // ボタンを配置するY座標の初期値
        float[] yPositions = new float[] { 450f, 0f, -450f };
        string[] labels = new string[] { "上", "真ん中", "下" }; // 上、真ん中、下のラベル

        // 新しいボタンを作成
        for (int i = 0; i < nextIndices.Count; i++)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();

            // インデックスに応じてラベルを設定
            button.GetComponentInChildren<TextMeshProUGUI>().text = labels[i];

            // ボタンの位置調整 (X座標を375、Y座標をyPositionsで変更)
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = new Vector2(375f, yPositions[i]);  // X = 375, YはyPositions[i]

            // ボタンがクリックされたときの処理
            int index = nextIndices[i];  // 関数内のインデックスを使うためにローカル変数に格納
            button.onClick.AddListener(() =>
            {
                OnBranchSelected?.Invoke(index);
                HideBranchOptions();
            });
        }
    }

    // 分岐オプションを非表示
    public void HideBranchOptions()
    {
        branchPanel.SetActive(false);
    }
}
