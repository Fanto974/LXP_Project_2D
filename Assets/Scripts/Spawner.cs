using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public float delay = 5.0f;
    public float delayRange = 2.0f;
    public float xRange = 10f;
    public float yRange = 10f;
    public GameObject[] ennemi;
    public int nbEnnemisTot = 10;
    public int nbEnnemisCur = 0;

    public bool wasActive = false;
    public GameObject doors;
    public GameObject[] Grids;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //door.gameObject.SetActive(false);
        doors.GetComponent<TilemapCollider2D>().isTrigger = true;
        
        foreach (GameObject child in Grids)
        {
            child.GetComponent<Animator>().SetBool("IsFighting", false);
        }

        xRange = Mathf.Abs(transform.position.x - transform.Find("XY").transform.position.x);
        yRange = Mathf.Abs(transform.position.y - transform.Find("XY").transform.position.y);
        
    }

    IEnumerator SpawnRoutine()
    {
        while (nbEnnemisCur <= nbEnnemisTot)
        {
            float effectiveDelay = delay + Random.Range(-delayRange, delayRange);
            Vector2 spawnPos = new Vector2(
                transform.position.x + Random.Range(-xRange, xRange),
                transform.position.y + Random.Range(-yRange, yRange)
            );
            yield return new WaitForSeconds(effectiveDelay);
            int nb = Random.Range(0, ennemi.Length);
            Instantiate(ennemi[nb], spawnPos, Quaternion.identity);
            nbEnnemisCur ++;
        }
        //door.gameObject.SetActive (false);
        doors.GetComponent<TilemapCollider2D>().isTrigger = true;
        foreach (GameObject child in Grids)
        {
            child.GetComponent<Animator>().SetBool("IsFighting", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && wasActive == false)
        {
            StartCoroutine(SpawnRoutine());
            //door.gameObject.SetActive(true);
            doors.GetComponent<TilemapCollider2D>().isTrigger = false;
            foreach (GameObject child in Grids)
            {
                child.GetComponent<Animator>().SetBool("IsFighting", true);
            }
            wasActive = true;
        }
    }
}