using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlus : MonoBehaviour, InterfaceItem
{
    public float nbDamageMore = 1;

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
            player.gameObject.GetComponent<PlayerMouvment>().bonusDamage += nbDamageMore;
            Destroy(this.gameObject);
        }
    }
}
