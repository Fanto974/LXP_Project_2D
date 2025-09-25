using UnityEngine;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance;

    public TMP_Text promptText;

    void Awake()
    {
        Instance = this;
        SetupPromptUI();
        HidePrompt();
    }

    public void SetupPromptUI()
    {
        RectTransform rt = promptText.GetComponent<RectTransform>();

        // Ancré en bas-centre
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);

        // Position verticale (5% de la hauteur)
        float marginY = Screen.height * 0.05f;
        rt.anchoredPosition = new Vector2(0, marginY);

        // Largeur = 80% de l’écran
        float width = Screen.width * 0.8f;
        float height = Screen.height * 0.1f; // hauteur auto (10% de l’écran)
        rt.sizeDelta = new Vector2(width, height);

        // Taille du texte (2% de la hauteur d’écran)
        promptText.fontSize = Mathf.RoundToInt(Screen.height * 0.04f);

        // Centrer le texte
        promptText.alignment = TMPro.TextAlignmentOptions.Center;
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
