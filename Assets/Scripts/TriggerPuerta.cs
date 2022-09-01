using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPuerta : MonoBehaviour
{
    [SerializeField] TriggersMapa triggersMapa;
    [SerializeField] bool cruzaTrigger = false;
    [SerializeField] GameObject enemy;
    [SerializeField] UIMenuPrincipal _menu;
    [SerializeField] AudioClip _audio;
    [SerializeField] AudioSource _audSource;
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorBroken;
    [SerializeField] GameObject crystal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(transform.name == "Trigger1")
            {
                triggersMapa.ToggleGameObjects(triggersMapa.cams,0);
            }

            if(transform.name == "Trigger2")
            {
                triggersMapa.ToggleGameObjects(triggersMapa.cams,1);
            }

            if(transform.name == "Trigger3")
            {
                triggersMapa.ToggleGameObjects(triggersMapa.cams,2);
                enemy.SetActive(true);
            }

            if(transform.name == "Trigger4")
            {
                triggersMapa.ToggleGameObjects(triggersMapa.cams,3);
                enemy.SetActive(true);
            }

            if(transform.name == "Trigger5")
            {
                StartCoroutine(_menu.SiguienteNivel(3));
            }

            if(transform.name == "Trigger6")
            {
                crystal.SetActive(false);
                door.SetActive(false);
                doorBroken.SetActive(true);
                _audSource.clip = _audio;
                _audSource.Play();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(transform.name == "Trigger1")
            {
                triggersMapa.cams[0].SetActive(false);
            }

            if(transform.name == "Trigger2")
            {
                triggersMapa.cams[1].SetActive(false);
            }

            if(transform.name == "Trigger3")
            {
                triggersMapa.cams[2].SetActive(false);
                enemy.SetActive(false);
            }

            if(transform.name == "Trigger4")
            {
                triggersMapa.cams[3].SetActive(false);
                enemy.SetActive(false);
            }
        }
    }
}
