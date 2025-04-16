using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public float price;
    public string description;
    private GameObject obj;
    public GameObject item;
    public bool wasUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj != null)
        {
            if (obj.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && wasUsed == false)
            {
                if (price <= obj.gameObject.GetComponent<PlayerHealth>().currentPieces)
                {
                    obj.gameObject.GetComponent<PlayerHealth>().currentPieces -= price;
                    print(item);
                    InterfaceItem interfaceitem = item.gameObject.GetComponent<InterfaceItem>();
                    interfaceitem.use(obj.gameObject);
                    wasUsed = true;
                }
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
