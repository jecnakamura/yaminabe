using UnityEngine;

public class IngredientUIManager : MonoBehaviour
{
    public GameObject ingredientPanel; // �H�ވꗗUI�p�l��
    public Transform ingredientContent; // ���X�g�̐e�I�u�W�F�N�g
    public GameObject ingredientItemPrefab; // �H�ލ��ڂ̃v���n�u
    /*
    public void ShowIngredients(PlayerData player)
    {
        // ��x���X�g���N���A
        foreach (Transform child in ingredientContent)
        {
            Destroy(child.gameObject);
        }

        // ���X�g���쐬
        foreach (Ingredient ingredient in player.ownedIngredients)
        {
            GameObject item = Instantiate(ingredientItemPrefab, ingredientContent);
            item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{ingredient.name} ({ingredient.genre})";
        }

        // �p�l����\��
        ingredientPanel.SetActive(true);
    }
    */
    public void CloseIngredientPanel()
    {
        ingredientPanel.SetActive(false);
    }
}
