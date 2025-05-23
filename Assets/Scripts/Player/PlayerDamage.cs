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

        // Mise � jour fluide de la barre de vie
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        healthBarFill.fillAmount = displayedHealth / maxHealth;
        vie.text = Mathf.FloorToInt(displayedHealth).ToString() + " / " + Mathf.FloorToInt(maxHealth).ToString();

        // Mise � jour des items
        pieces.text = Mathf.FloorToInt(currentPieces).ToString();

        if (obj != null)
        {
            InterfaceItem interfaceItem = obj.GetComponent<InterfaceItem>();
            if (interfaceItem != null) {
                interfaceItem.use(this.gameObject);
                obj = null;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            currentPieces++;
            Destroy(collision.gameObject);
        }
        else
            obj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        obj = null;
    }


}
