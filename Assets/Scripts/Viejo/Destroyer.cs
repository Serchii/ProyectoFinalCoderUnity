using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private RoomTemplates templates;
    // Start is called before the first frame update
    void Start() 
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if(gameObject.name == "CentralFloor")
        {
            Instantiate(templates.floorPrincipal,transform.position,transform.rotation);
        }
        else
        {
            Instantiate(templates.floor,transform.position,transform.rotation);
        }
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("SpawnPoint"))
            Destroy(other.gameObject);
    }
}
