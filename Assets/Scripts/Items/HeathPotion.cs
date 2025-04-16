using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class HeathPotion : MonoBehaviour, InterfaceItem
{
    public float heal = 10f;
    private PlayerHealth joueur;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void use(GameObject player)
    {
        joueur = player.gameObject.GetComponent<PlayerHealth>();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (joueur.currentHealth + heal >= joueur.maxHealth)
                joueur.currentHealth = joueur.maxHealth;
            else
                joueur.currentHealth += heal;
            Destroy(this.gameObject);
        }
    }
}
