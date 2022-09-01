using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openSide;
    int rand;
    bool spawned = false;
    public int limiteRooms = 0;
    

    //1 down
    //2 left
    //3 up   ||
    //4 right

    private RoomTemplates templates;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if(!spawned)
        {
            switch(openSide)
            {
                case 1:
                    /*if(templates.cantRooms < limiteRooms)
                    {*/
                        rand = Random.Range(0,templates.down.Length);
                    /*}
                    else
                    {
                        rand = Random.Range(0,templates.down.Length-3);
                    }*/
                    Instantiate(templates.down[rand], transform.position, templates.down[rand].transform.rotation);
                    break;
                case 2: 
                    /*if(templates.cantRooms < limiteRooms)
                    {*/
                        rand = Random.Range(0,templates.left.Length);
                    /*}
                    else
                    {
                        rand = Random.Range(0,templates.left.Length-3);
                    }*/
                    Instantiate(templates.left[rand], transform.position, templates.left[rand].transform.rotation);
                    break;
                case 3:
                    /*if(templates.cantRooms < limiteRooms)
                    {*/
                        rand = Random.Range(0,templates.up.Length);
                    /*}
                    else
                    {
                        rand = Random.Range(0,templates.up.Length-3);
                    }*/
                    Instantiate(templates.up[rand], transform.position, templates.up[rand].transform.rotation);
                    break;
                case 4:
                    /*if(templates.cantRooms < limiteRooms)
                    {*/
                        rand = Random.Range(0,templates.right.Length);
                    /*}
                    else
                    {
                        rand = Random.Range(0,templates.right.Length-3);
                    }*/
                    Instantiate(templates.right[rand], transform.position, templates.right[rand].transform.rotation);
                    break;
            }

            spawned = true;
            templates.cantRooms++;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned != true && spawned != true)
            {
                Instantiate(templates.emptyRoom,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }

           spawned = true;
        }
    
    }

}
