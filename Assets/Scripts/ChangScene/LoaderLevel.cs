using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderLevel : MonoBehaviour
{
    // Salles
    public GameObject[] roomPrefabs;
    private GameObject[] generatedRooms;
    public GameObject spawnRoom;
    public GameObject portalRoom;
    public GameObject MurVertical;
    public GameObject MurHorizontal;
    // Compteurs de Salles
    public int nbFightRoomsAllowed;
    private int nbFightRooms;
    public int nbItemsRoomsAllowed;
    private int nbItemsRooms;
    public int nbChessRoomsAllowed;
    private int nbChessRooms;
    // Positions
    public Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos.x = 0;
        currentPos.y = 0;
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRooms()
    {
        GameObject room = Instantiate(spawnRoom, currentPos, Quaternion.identity);
        //while (nbChessRooms <= nbChessRoomsAllowed || nbFightRooms <= nbFightRoomsAllowed || nbItemsRooms < nbItemsRoomsAllowed)
        //{

        //} 

    }
}
