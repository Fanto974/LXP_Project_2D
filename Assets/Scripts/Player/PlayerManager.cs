using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Classes
    public PlayerMouvment mouv;
    public PlayerAttack attack;

    public TextMeshProUGUI pieces;
    public int currentPieces = 0;

    public List<ShopItem> inventory = new List<ShopItem>();


    // Start is called before the first frame update
    void Start()
    {
        if (attack == null) attack = GetComponent<PlayerAttack>();
        if (mouv == null) mouv = GetComponent<PlayerMouvment>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mettre à jour le nombre de Pièces
        UpdateNbGold();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            CollectGold(collision);
        }
    }


    //
    //
    //
    public void UpdateNbGold()
    {
        pieces.text = Mathf.FloorToInt(currentPieces).ToString();
    }

    public void CollectGold(Collider2D pièce)
    {
        currentPieces++;
        Destroy(pièce.gameObject);
    }

    public bool CanAfford(int price) => currentPieces >= price;

    public void SpendCoins(int amount)
    {
        currentPieces -= amount;
    }

    public void AddItem(ShopItem item)
    {
        inventory.Add(item);
        // Tu peux ajouter ici des effets (arme, potion…)
    }
}
