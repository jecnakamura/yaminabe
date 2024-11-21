using UnityEngine;

public class IngredientUIManager : MonoBehaviour
{
    public GameObject ingredientPanel; // 食材一覧UIパネル
    public Transform ingredientContent; // リストの親オブジェクト
    public GameObject ingredientItemPrefab; // 食材項目のプレハブ
    /*
    public void ShowIngredients(PlayerData player)
    {
        // 一度リストをクリア
        foreach (Transform child in ingredientContent)
        {
            Destroy(child.gameObject);
        }

        // リストを作成
        foreach (Ingredient ingredient in player.ownedIngredients)
        {
            GameObject item = Instantiate(ingredientItemPrefab, ingredientContent);
            item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{ingredient.name} ({ingredient.genre})";
        }

        // パネルを表示
        ingredientPanel.SetActive(true);
    }
    */
    public void CloseIngredientPanel()
    {
        ingredientPanel.SetActive(false);
    }
}
