using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float h;
    public float v;
    public bool isMoving;
    public bool isAttacking = false;
    public bool isDead = false;

    public Transform target; // Player
    public NavMeshAgent agent;

    public float range = 2f;

    public float damage = 5;
    public float health = 10;

    public GameObject prefabPiece;

    private Vector2 moveDir;
    private Vector2 oldPos;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        oldPos = transform.position;
    }

    void Update()
    {
        if (!isDead)
        {
            // Calcule la direction entre l'ennemi et le joueur
            moveDir = ((Vector2)this.transform.position - oldPos).normalized;
            // Calcul la distance entre l'ennemi et le joueur
            float distance = Vector2.Distance(transform.position, target.position);

            // Mets à jour les paramètres de l'animator
            isMoving = moveDir.x != 0 || moveDir.y != 0;

            if (isMoving)
            {
                h = moveDir.x;
                v = moveDir.y;
            }

            if (distance <= range)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }

            animator.SetFloat("Horizontal", h);
            animator.SetFloat("Vertical", v);
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("isAttacking", isAttacking);

            // Déplace l'ennemi vers le joueur
            agent.SetDestination(target.position);

            oldPos = transform.position;
        }
        
    }

    public void takeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0) {
            StartCoroutine(Mort(10));
        }
        else
        {
            animator.SetTrigger("IsTakingDamage");
        }
    }

    IEnumerator Mort(float secondes)
    {
        isDead = true;

        Instantiate(prefabPiece, this.transform.position, this.transform.rotation);
        animator.SetTrigger("IsDead");

        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        yield return new WaitForSeconds(secondes);
        Destroy(this.gameObject);
    }
}
