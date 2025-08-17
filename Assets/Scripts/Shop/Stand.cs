using UnityEngine;
using static UnityEditor.Progress;

public class ShopStand : MonoBehaviour
{
    public ShopItem item; // L’objet vendu sur ce stand
    private GameObject itemGO;
    public Vector3 itemOffset = new Vector3(0, 1f, 0);
    public Vector3 itemScale = new Vector3(5f, 5f, 5f);
    public Vector3 standScale = new Vector3(1f, 1f, 1f);

    private bool isPlayerNearby = false;
    private PlayerManager player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerManager>();
            isPlayerNearby = true;
            ShopUIManager.Instance.ShowPrompt(item); // Affiche “Appuie sur F pour acheter…”
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            ShopUIManager.Instance.HidePrompt();
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            TryBuy();
        }
    }

    private void Start()
    {
        this.transform.localScale = standScale;

        if (item != null && item.icon != null)
        {
            // Crée un GameObject enfant pour afficher le sprite
            itemGO = new GameObject(item.itemName + "_Visual");
            itemGO.transform.parent = transform;
            itemGO.transform.localPosition = itemOffset;

            itemGO.transform.localScale = itemScale;

            // Ajoute un SpriteRenderer et assigne l’icône
            SpriteRenderer sr = itemGO.AddComponent<SpriteRenderer>();
            sr.sprite = item.icon;
            sr.sortingOrder = 1; // pour être au-dessus du stand
        }
    }

    void TryBuy()
    {
        if (player.CanAfford(item.price))
        {
            player.SpendCoins(item.price);
            player.AddItem(item);
            item.ApplyEffect(player);
            Debug.Log("Acheté : " + item.itemName);

            ShopUIManager.Instance.HidePrompt();
            Destroy(itemGO); // L’objet disparaît après achat
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            Debug.Log("Pas assez de pièces !");
        }
    }
}
