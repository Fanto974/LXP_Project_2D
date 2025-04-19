using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoaderLevel : MonoBehaviour
{
    // Salles
    public GameObject[] fightRoomPrefabs;
    public GameObject[] itemsRoomPrefabs;
    public GameObject[] chessRoomPrefabs;
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

    // Offset entre chaque salle (ex : 20x20 unités)
    public Vector2 roomOffset = new Vector2(20, 0);

    // Start is called before the first frame update
    void Start()
    {
        currentPos = Vector2.zero;
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRooms()
    {
        Instantiate(spawnRoom, currentPos, Quaternion.identity);

        // Avancer la position pour la prochaine salle
        currentPos += roomOffset;

        // Génération jusqu’à atteindre le nombre max de chaque type de salle
        while (nbFightRooms < nbFightRoomsAllowed || nbItemsRooms < nbItemsRoomsAllowed || nbChessRooms < nbChessRoomsAllowed)
        {
            // Choisir une salle au hasard
            GameObject selectedRoom = null;

            // Choisir un type en fonction des salles encore disponibles
            int type = Random.Range(0, 3); // 0 = fight, 1 = item, 2 = chess

            if (type == 0 && nbFightRooms < nbFightRoomsAllowed)
            {
                int nb = Random.Range(0, fightRoomPrefabs.Length);
                selectedRoom = fightRoomPrefabs[nb]; // fight
                nbFightRooms++;
            }
            else if (type == 1 && nbItemsRooms < nbItemsRoomsAllowed)
            {
                int nb = Random.Range(0, itemsRoomPrefabs.Length);
                selectedRoom = itemsRoomPrefabs[nb]; // item
                nbItemsRooms++;
            }
            else if (type == 2 && nbChessRooms < nbChessRoomsAllowed)
            {
                int nb = Random.Range(0, chessRoomPrefabs.Length);
                selectedRoom = chessRoomPrefabs[nb]; // chess
                nbChessRooms++;
            }
            else
            {
                continue; // retry avec une autre salle
            }

            // Placer la salle
            Instantiate(selectedRoom, currentPos, Quaternion.identity);
            currentPos += roomOffset; // avancer
        }

        // Placer la salle de fin (portal)
        Instantiate(portalRoom, currentPos, Quaternion.identity);
    }

}

