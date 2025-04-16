using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public float delay = 5.0f;
    public float delayRange = 2.0f;
    public float posRange = 20.0f;
    public GameObject ennemi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float effectiveDelay = delay + Random.Range(-delayRange, delayRange);
            Vector2 spawnPos = new Vector2(
                transform.position.x + Random.Range(-posRange, posRange),
                transform.position.y + Random.Range(-posRange, posRange)
            );
            yield return new WaitForSeconds(effectiveDelay);
            Instantiate(ennemi, spawnPos, Quaternion.identity);
        }
    }
}