using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public Transform target; // Player
    public NavMeshAgent agent;

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
        // Calcule la direction entre l'ennemi et le joueur
        moveDir = ((Vector2)this.transform.position - oldPos).normalized;

        // Mets à jour les paramètres de l'animator
        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
        animator.SetFloat("Speed", moveDir.sqrMagnitude);

        // Déplace l'ennemi vers le joueur
        agent.SetDestination(target.position);

        oldPos = transform.position;
    }

    public void takeDamage(float damage)
    {
        this.health -= damage;

        if (this.health <= 0) {
            Instantiate(prefabPiece, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
