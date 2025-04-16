using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowController : MonoBehaviour
{
    public float speed;
    Vector2 dir;

    public float damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        dir = (transform.right + transform.up).normalized;
        this.transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyController>().takeDamage(this.damage);
        }
    }

}
