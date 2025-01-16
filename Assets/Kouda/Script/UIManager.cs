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
    public void ShowBranchOptions(List<int> nextIndices,Player player)
    {
        branchPanel.SetActive(true);

        // 古いボタンを削除
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // 初期位置（Y軸）の設定
        float buttonHeight = 50f;  // 各ボタンの高さ（仮の値）
        float offsetY = 0f;

        // 新しいボタンを作成
        foreach (int index in nextIndices)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"マス {index}";

            // ボタンの位置をずらす
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = new Vector2(0, offsetY);

            // ボタンがクリックされたときの処理
            button.onClick.AddListener(() =>
            {
                OnBranchSelected?.Invoke(index);
                HideBranchOptions();
            });

            // 次のボタンの位置をずらす
            offsetY -= buttonHeight;  // ボタンを下に配置するためにY軸方向にオフセット
        }
    }

    // 分岐オプションを非表示
    public void HideBranchOptions()
    {
        branchPanel.SetActive(false);
    }
}
