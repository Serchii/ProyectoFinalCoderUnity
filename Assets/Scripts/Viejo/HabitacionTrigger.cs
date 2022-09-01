using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HabitacionTrigger : MonoBehaviour
{
    public GameObject camaraHabitacion;
    public GameObject enemy;
    GameObject enemyAux;
    [SerializeField] GameObject enemies;
    [SerializeField] int cantEnemy;
    [SerializeField] RoomBehavior room;
    [SerializeField] float timer;

    // Start is called before the first frame update
    void Start()
    {
        float randZ;
        float randX;
        Vector3 spawnEnemy;

        cantEnemy = Random.Range(1,5);

        for(int i = 0; i < cantEnemy; i++)
        {
            randZ = Random.Range(transform.position.z-4,transform.position.z+4);
            randX = Random.Range(transform.position.x-8,transform.position.x+8);

            spawnEnemy = new Vector3(randX,0,randZ);

            enemyAux = Instantiate(enemy,spawnEnemy,transform.rotation);

            enemyAux.transform.SetParent(enemies.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.gameObject.tag == "Player")
        {
            timer = 0;
            ToggleEnemies(true);
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            
            camaraHabitacion.SetActive(true);
            UIJuego.camaraActiva = camaraHabitacion;
            
            room.ToggleGameObjects(room.lamps,true);
        }

        if(other.transform.gameObject.tag == "Enemigo")
        {
            timer+=Time.deltaTime;
            
            if(timer > 1)
            room.ToggleGameObjects(room.doorsClosed,true);
        }
        else
        {
            room.ToggleGameObjects(room.doorsClosed,false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if(other.transform.gameObject.tag == "Player")
        {
            timer = 0;
            ToggleEnemies(false);
            camaraHabitacion.SetActive(false);
            room.ToggleGameObjects(room.doorsClosed,false);
            room.ToggleGameObjects(room.lamps,false);
        }
    }


    void ToggleEnemies(bool status)
    {
        enemies.SetActive(status);
    }

}
