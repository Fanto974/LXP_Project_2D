using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    // Pour la vie
    public float maxHealth = 100f;
    public float currentHealth;
    private float displayedHealth;
    public Image healthBarFill;
    public TextMeshProUGUI vie;
    public float lerpSpeed = 5f;

    // Pour les attaques
    public GameObject arrowPrefab;
    public Transform DPSZone;
    public int damage = 10;
    public LayerMask enemyLayer;
    public int range = 5;



    // Fonctions
    // Basiques
    // Seulements
    void Start()
    {
        DPSZone = this.transform.Find("PtAtt");
        currentHealth = maxHealth;
        displayedHealth = maxHealth;
        vie.text = Mathf.FloorToInt(maxHealth).ToString() + " / " + Mathf.FloorToInt(maxHealth).ToString();
    }

    void Update()
    {
        // Pour mettre à jour la barre de vie
        UpdateHealthBar();

        // Pour attaquer
        if (Input.GetMouseButtonDown(0))
        {
            SwordAtt();
        }

        // Pour tester : appuyez sur H pour perdre 10 points de vie
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }
    }



    // Autres
    // Fonctions
    // Seulement
    public void SwordAtt()
    {
        animator.SetBool("IsClicking", true);

        // Détecter les ennemis dans la zone d’attaque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(DPSZone.position, range, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            if (ec != null)
                ec.takeDamage(damage);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void UpdateHealthBar()
    {
        displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * lerpSpeed);
        healthBarFill.fillAmount = displayedHealth / maxHealth;
        vie.text = Mathf.FloorToInt(displayedHealth).ToString() + " / " + Mathf.FloorToInt(maxHealth).ToString();
    }

    void OnDrawGizmosSelected()
    {
        if (DPSZone == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DPSZone.position, range);
    }

    /*
    IEnumerator ShootArrowDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;
        ArrowController arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angle - 45)).GetComponent<ArrowController>();
        arrow.damage += bonusDamage;
    }*/

}
