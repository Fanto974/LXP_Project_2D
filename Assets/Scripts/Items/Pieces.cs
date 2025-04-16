using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour, InterfaceItem
{
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
        player.gameObject.GetComponent<PlayerHealth>().currentPieces++;
        Destroy(this.gameObject);
    }
}
