using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPlus : MonoBehaviour, InterfaceItem
{
    public float nbHpMore = 10;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.gameObject.GetComponent<PlayerHealth>().maxHealth += nbHpMore;
            Destroy(this.gameObject);
        }
    }
}
