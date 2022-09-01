using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; // 0 - up - 1 - right - 2 - down - 3 - left
    public GameObject[] doors;
    public GameObject[] doorsClosed;
    public GameObject[] lamps;
   

    // Update is called once per frame
    public void UpdateRoom(bool[] status)
    {
        for(int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    

    public void ToggleGameObjects(GameObject[] objects, bool status)
    {
        
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(status);
        }
        
        
    }

    
}
