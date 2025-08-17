using UnityEngine;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance;

    public TMP_Text promptText;

    void Awake()
    {
        Instance = this;
        HidePrompt();
    }

    public void ShowPrompt(ShopItem item)
    {
        promptText.text = $"Appuie sur F pour acheter {item.itemName} ({item.price} pièces)";
        promptText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
