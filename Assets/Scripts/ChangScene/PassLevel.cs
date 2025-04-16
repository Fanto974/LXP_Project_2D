using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{
    public string nbLevel;
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + nbLevel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LoadLevel();
    }
}
