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
    public GameObject[] portalRoomPrefabs;
    private GameObject[] generatedRooms;
    public GameObject spawnRoom;
    public GameObject MurVertical;
    public GameObject MurHorizontal;
    // Compteurs de Salles
    public int nbFightRoomsAllowed;
    private int nbFightRooms;
    public int nbItemsRoomsAllowed;
    private int nbItemsRooms;
    public int nbChessRoomsAllowed;
    private int nbChessRooms;
    public int nbPortalRoomsAllowed;
    private int nbPortalRooms;
    // Positions
    public Vector2 currentPos;
    private HashSet<Vector2> occupiedPos = new HashSet<Vector2>();


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
        occupiedPos.Add(currentPos);
        file.Enqueue(sr);

        while (file.Count > 0)
        {
            currentRoom = file.Dequeue();
            GameObject portes = currentRoom.transform.Find("Map/Portes").gameObject;
            do
            {
                foreach (Transform porteTrans in portes.transform)
                {
                    if (nbFightRooms < nbFightRoomsAllowed || nbItemsRooms < nbItemsRoomsAllowed || nbChessRooms < nbChessRoomsAllowed || nbPortalRooms < nbPortalRoomsAllowed)
                    {
                        GameObject porte = porteTrans.gameObject;
                        if (porte.activeSelf)
                        {
                            int chanceNewRoom = Random.Range(0, 3);
                            if (chanceNewRoom == 0)
                            {
                                currentPos = currentRoom.transform.position;
                                GameObject newRoom = null;
                                Vector2 offset = new Vector2(0, 0);
                                string door = "";
                                GameObject wall = null;

                                if (porte.name == "PorteN")
                                {
                                    offset = new Vector2(0, 20);
                                    door = "PorteS";
                                    wall = MurVertical;
                                }
                                if (porte.name == "PorteE")
                                {
                                    offset = new Vector2(20, 0);
                                    door = "PorteO";
                                    wall = MurHorizontal;
                                }
                                if (porte.name == "PorteS")
                                {
                                    offset = new Vector2(0, -20);
                                    door = "PorteN";
                                    wall = MurVertical;
                                }
                                if (porte.name == "PorteO")
                                {
                                    offset = new Vector2(-20, 0);
                                    door = "PorteE";
                                    wall = MurHorizontal;
                                }

                                Vector2 newRoomPos = (Vector2)currentRoom.transform.position + offset;

                                if (!occupiedPos.Contains(newRoomPos))
                                {
                                    newRoom = CreateRoom(offset, door, wall);
                                    occupiedPos.Add(newRoomPos);
                                    porte.SetActive(false);
                                    if (currentRoom.CompareTag("FightRoom"))
                                    {
                                        currentRoom.transform.Find("Spawner").gameObject.GetComponent<EnemySpawner>().doors.Add(porte);
                                    }
                                    file.Enqueue(newRoom);
                                }

                                
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

    }






    public GameObject CreateRoom(Vector2 roomOffset, string entree, GameObject mur)
    {
        print(entree);
        // Choisir une salle au hasard
        GameObject selectedRoom = null;

        // Choisir un type en fonction des salles encore disponibles

        while (selectedRoom == null) 
        {
            int type = Random.Range(0, 4); // 0 = fight, 1 = item, 2 = chess

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
            else if (type ==3 && nbPortalRooms < nbPortalRoomsAllowed)
            {
                int nb = Random.Range(0, portalRoomPrefabs.Length);
                selectedRoom = portalRoomPrefabs[nb]; // portal
                nbPortalRooms++;
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
            Debug.LogWarning("La porte ciblée est le parent ou n'existe pas !");
        }

        portes.SetActive(true);
        return currentRoom;
    }

}

