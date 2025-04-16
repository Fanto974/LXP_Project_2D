using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMouvment : MonoBehaviour { 

    public float mouvSpeed;
    private Rigidbody2D rb;
    private Vector2 mouvDirection;

    public GameObject arrowPrefab;

    public Animator animator;

    public Vector2 lastMoveDir = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouvDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (mouvDirection != Vector2.zero)
            lastMoveDir = mouvDirection;


        animator.SetFloat("Horizontal", mouvDirection.x);
        animator.SetFloat("Vertical", mouvDirection.y);
        animator.SetFloat("Speed", mouvDirection.sqrMagnitude);

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsClicking", true);
            StartCoroutine(ShootArrowDelayed(0.2f));
            

        }

    }

    IEnumerator ShootArrowDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angle - 45));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + mouvDirection * mouvSpeed * Time.fixedDeltaTime);
    }
}
