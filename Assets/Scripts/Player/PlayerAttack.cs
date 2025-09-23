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
    public bool isDead = false;

    // Pour le Mana
    public int maxMana = 100;
    public int currentMana;
    public Image manaBarImage;
    public Sprite[] manaSprites;

    // Pour les attaques
    public List<AttackEffect> attackEffects = new List<AttackEffect>();
    public Transform DPSZone;
    public LayerMask enemyLayer;
    public int range = 5;
    public int damage = 10;



    // Fonctions
    // Basiques
    // Seulements
    void Start()
    {
        DPSZone = this.transform.Find("PtAtt");
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateHealthBar();
        UpdateManaBar();
    }

    void Update()
    {

        // Pour attaquer
        if (Input.GetMouseButtonDown(0))
        {
            SwordAtt();
        }

        // Pour tester : appuyez sur H pour perdre 1 points de vie
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }

        // Pour tester : appuyez sur J pour perdre 10 de mana
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeMana(10);
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
                    if (currentMana >= effect.costManaAtt) {
                        effect.Apply(ec);
                        TakeMana(effect.costManaAtt);
                    }
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void TakeMana(int amount)
    {
        currentMana -= amount;
        UpdateManaBar();
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void HMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        maxHealth += amount;
    }

    public void GainMana(int amount)
    {
        currentMana += amount;
        maxMana += amount;
    }

    public void GainDamage(int amount)
    {
        damage += amount;
    }

    public void UpdateHealthBar()
    {
        if (healthSprites.Length == 0) return;

        // Convertit la vie actuelle en index entre 0 et le dernier sprite
        int index = Mathf.RoundToInt((float)currentHealth / maxHealth * (healthSprites.Length - 1));

        index = Mathf.Clamp(index, 0, healthSprites.Length - 1);
        healthBarImage.sprite = healthSprites[index];
    }

    public void UpdateManaBar()
    {
        if (manaSprites.Length == 0) return;

        // Convertit la vie actuelle en index entre 0 et le dernier sprite
        int index = Mathf.RoundToInt((float)currentMana / maxMana * (manaSprites.Length - 1));

        index = Mathf.Clamp(index, 0, manaSprites.Length - 1);
        manaBarImage.sprite = manaSprites[index];
    }

    void OnDrawGizmosSelected()
    {
        if (DPSZone == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DPSZone.position, range);
    }

    public void AddManaComp(ShopItem Comp)
    {
        Competence C = (Competence)Comp;
        attackEffects.Add(C.AttackEffect);
        // Tu peux ajouter ici des effets (arme, potion…)
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
