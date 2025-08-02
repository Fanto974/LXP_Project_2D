using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public TextMeshProUGUI pieces;
    public float currentPieces = 0;


    // Start is called before the first frame update
    void Start()
    {

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
}
