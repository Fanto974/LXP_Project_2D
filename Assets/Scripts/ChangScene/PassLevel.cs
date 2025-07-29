using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PassLevel : MonoBehaviour
{
    private GameObject obj;
    private int index;

    public void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex; // Pas besoin vu que je fait tout dans la même scène mais je le laisse si jamais
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    void Update()
    {
        if (obj != null)
        {
            if (obj.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
            {
                LoadLevel();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        obj = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        obj = null;
    }
}
