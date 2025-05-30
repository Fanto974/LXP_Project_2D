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
        Queue<GameObject> file = new Queue<GameObject>();
        GameObject currentRoom = null;

        GameObject sr = Instantiate(spawnRoom, currentPos, Quaternion.identity);
        file.Enqueue(sr);

        while (file.Count > 0)
        {
            currentRoom = file.Dequeue();
            GameObject portes = currentRoom.transform.Find("Map/Portes").gameObject;
            do
            {
                foreach (Transform porteTrans in portes.transform)
                {
                    if (nbFightRooms < nbFightRoomsAllowed || nbItemsRooms < nbItemsRoomsAllowed || nbChessRooms < nbChessRoomsAllowed)
                    {
                        GameObject porte = porteTrans.gameObject;
                        if (porte.activeSelf)
                        {
                            int chanceNewRoom = Random.Range(0, 3);
                            if (chanceNewRoom == 0)
                            {
                                currentPos = currentRoom.transform.position;
                                GameObject newRoom = null;
                                if (porte.name == "PorteN")
                                {
                                    newRoom = CreateRoom(new Vector2(0, 20), "PorteS", MurVertical);
                                }
                                if (porte.name == "PorteE")
                                {
                                    newRoom = CreateRoom(new Vector2(20, 0), "PorteO", MurHorizontal);
                                }
                                if (porte.name == "PorteS")
                                {
                                    newRoom = CreateRoom(new Vector2(0, -20), "PorteN", MurVertical);
                                }
                                if (porte.name == "PorteO")
                                {
                                    newRoom = CreateRoom(new Vector2(-20, 0), "PorteE", MurHorizontal);
                                }
                                porte.SetActive(false);
                                if (currentRoom.CompareTag("FightRoom"))
                                {
                                    currentRoom.transform.Find("Spawner").gameObject.GetComponent<EnemySpawner>().doors.Add(porte);
                                }
                                file.Enqueue(newRoom);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            } while (file.Count == 0 && nbFightRooms < nbFightRoomsAllowed || nbItemsRooms < nbItemsRoomsAllowed || nbChessRooms < nbChessRoomsAllowed);
                
        }
        
        // Placer la salle de fin (portal)
        Instantiate(portalRoom, currentPos, Quaternion.identity);
    }






    public GameObject CreateRoom(Vector2 roomOffset, string entree, GameObject mur)
    {
        print(entree);
        // Choisir une salle au hasard
        GameObject selectedRoom = null;

        // Choisir un type en fonction des salles encore disponibles

        while (selectedRoom == null) 
        {
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
        }
        

        // Placer la salle
        currentPos += roomOffset / 2; // avancer
        Instantiate(mur, currentPos, Quaternion.identity);
        currentPos += roomOffset / 2; // avancer
        GameObject currentRoom = Instantiate(selectedRoom, currentPos, Quaternion.identity);

        GameObject portes = currentRoom.transform.Find("Map/Portes").gameObject;
        portes.SetActive(true);
        Transform porte = portes.transform.Find(entree);

        if (porte != null && porte != portes.transform) // s'assurer que ce n'est pas "Portes"
        {
            porte.gameObject.SetActive(false);
            if (currentRoom.CompareTag("FightRoom"))
            {
                currentRoom.transform.Find("Spawner").gameObject.GetComponent<EnemySpawner>().doors.Add(porte.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("La porte cibl�e est le parent ou n'existe pas !");
        }

        portes.SetActive(true);
        return currentRoom;
    }

}

