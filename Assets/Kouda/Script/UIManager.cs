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
    public void ShowBranchOptions(List<int> nextIndices)
    {
        branchPanel.SetActive(true);

        // 古いボタンを削除
        foreach (Transform child in branchButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // 新しいボタンを作成
        foreach (int index in nextIndices)
        {
            GameObject buttonObj = Instantiate(branchButtonPrefab, branchButtonContainer);
            Button button = buttonObj.GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"マス {index}";

            // ボタンがクリックされたときの処理
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
