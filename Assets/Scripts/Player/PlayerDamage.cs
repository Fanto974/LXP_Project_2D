using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private float displayedHealth;
    public Image healthBarFill;
    public TextMeshProUGUI vie;
    public float lerpSpeed = 5f;


    public TextMeshProUGUI pieces;
    public float currentPieces = 0;

    public float nbHealPotion = 10;

    private bool isInCollision = false;
    private GameObject obj;

    void Start()
    {
        currentHealth = maxHealth;
        displayedHealth = maxHealth;
        vie.text = Mathf.FloorToInt(maxHealth).ToString() + " / " + Mathf.FloorToInt(maxHealth).ToString();
    }

    void Update()
    {
        // Pour tester : appuyez sur H pour perdre 10 points de vie
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }

        // Mise à jour fluide de la barre de vie
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        healthBarFill.fillAmount = displayedHealth / maxHealth;
        vie.text = Mathf.FloorToInt(displayedHealth).ToString() + " / " + Mathf.FloorToInt(maxHealth).ToString();

        // Mise à jour des pieces
        pieces.text = Mathf.FloorToInt(currentPieces).ToString();

        if (obj != null)
        {
            if (obj.CompareTag("Piece"))
            {
                currentPieces += 1;
                Destroy(obj);
                obj = null;
            }

            else if (obj.CompareTag("HealthPotion") && Input.GetKey(KeyCode.F))
            {
                currentHealth += nbHealPotion;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                Destroy(obj);
                obj = null;
            }

            else if (obj.CompareTag("Escaliers") && Input.GetKeyDown(KeyCode.F))
            {
                obj.GetComponent<EscDesc>().use(this.gameObject);
            }
        }
        

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemi"))
            {
                TakeDamage(collision.gameObject.GetComponent<EnemyController>().damage);

            }
        }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInCollision = true;
        obj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInCollision = false;
        obj = null;
    }


}
