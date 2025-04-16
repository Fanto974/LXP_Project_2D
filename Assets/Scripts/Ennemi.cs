using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public Transform target; // Player
    public float speed = 5.0f;

    public float damage = 5;
    public float health = 10;

    public GameObject prefabPiece;

    private Vector2 moveDir;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Calcule la direction entre l'ennemi et le joueur
        moveDir = (target.position - transform.position).normalized;

        // Mets à jour les paramètres de l'animator
        animator.SetFloat("Horizontal", moveDir.x);
        animator.SetFloat("Vertical", moveDir.y);
        animator.SetFloat("Speed", moveDir.sqrMagnitude);

        // Déplace l'ennemi vers le joueur
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
