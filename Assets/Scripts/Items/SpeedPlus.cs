using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlus : MonoBehaviour, InterfaceItem
{
    public float nbSpeedMore = 0.25f;

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
            player.gameObject.GetComponent<PlayerMouvment>().mouvSpeed += nbSpeedMore;
            Destroy(this.gameObject);
        }
    }
}
