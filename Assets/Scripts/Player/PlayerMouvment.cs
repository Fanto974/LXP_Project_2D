using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMouvment : MonoBehaviour { 
    // Pour le mouvement
    public float mouvSpeed;
    private Rigidbody2D rb;
    private Vector2 mouvDirection;

    // Pour les animations
    public Animator animator;
    public Vector2 lastMoveDir = Vector2.right;
    public float h;
    public float v;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouvDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        AnimMouv();
    }

    private void FixedUpdate()
    {
        Mouv();
    }

    public void Mouv()
    {
        rb.MovePosition(rb.position + mouvDirection * mouvSpeed * Time.fixedDeltaTime);
    }

    public void AnimMouv()
    {
        if (mouvDirection != Vector2.zero)
            lastMoveDir = mouvDirection;

        isMoving = mouvDirection.x != 0 || mouvDirection.y != 0;

        if (isMoving)
        {
            h = mouvDirection.x;
            v = mouvDirection.y;
        }

        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
        animator.SetBool("isMoving", isMoving);
        GetComponent<PlayerAttack>().DPSZone.position = this.transform.position + (Vector3)lastMoveDir;
    }

    public void GainSpeed(float speed)
    {
        mouvSpeed += speed;
    }
}