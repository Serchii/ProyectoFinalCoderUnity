using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseEvento : MonoBehaviour
{
    public static event Action disparo;
    public static event Action reinicio;
    public static event Action cambioColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            disparo?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            reinicio?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            cambioColor?.Invoke();
        }
    }
}
