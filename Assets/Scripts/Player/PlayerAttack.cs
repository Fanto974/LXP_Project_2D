using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    // Pour la vie
    public int maxHealth = 10;
    public int currentHealth;
    public Image healthBarImage;
    public Sprite[] healthSprites;

    // Pour les attaques
    public GameObject arrowPrefab;
    public Transform DPSZone;
    public int damage = 10;
    public LayerMask enemyLayer;
    public int range = 5;
    public List<AttackEffect> attackEffects = new List<AttackEffect>();



    // Fonctions
    // Basiques
    // Seulements
    void Start()
    {
        DPSZone = this.transform.Find("PtAtt");
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {

        // Pour attaquer
        if (Input.GetMouseButtonDown(0))
        {
            SwordAtt();
        }

        // Pour tester : appuyez sur H pour perdre 10 points de vie
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
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
            IEffectReceiver ec = enemy.GetComponent<IEffectReceiver>();
            if (ec != null)
            {
                ec.takeDamage(damage);
                foreach (var effect in attackEffects)
                {
                    effect.Apply(ec);
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        maxHealth += amount;
    }

    public void GainDamage(int amount)
    {
        damage += amount;
    }

    public void UpdateHealthBar()
    {
        print($"UpdateHealthBar call | currentHealth={currentHealth}, healthSprites.Length={healthSprites.Length}");

        print("fonctcall");
        // Sécurité : éviter erreurs si sprites manquants
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            print("fefsef");
            healthBarImage.sprite = healthSprites[currentHealth];
        }
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
