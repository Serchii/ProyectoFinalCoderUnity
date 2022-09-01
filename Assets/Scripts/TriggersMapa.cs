using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersMapa : MonoBehaviour
{
    [Header("Camaras")]
    public GameObject[] cams;

    [Header("Triggers")]
    public GameObject[] triggs;


    public void ToggleGameObjects(GameObject[] obj, int index)
    {
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
            if(i == index)
            {
                obj[i].SetActive(true);
            }
        }
    }

}
