using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscDesc : MonoBehaviour, InterfaceItem
{
    public GameObject esc;

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
            player.transform.position = esc.transform.position;
        }
    }
}
