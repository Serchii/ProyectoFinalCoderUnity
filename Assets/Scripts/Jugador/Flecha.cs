using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float speed = 25f;
    public Vector3 direction = new Vector3(0,0,1f);
    public UIJuego uiScript;
    public AudioSource _audio;
    public AudioClip _clip;


    void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        MovimientoProyectil();
    }

    protected void MovimientoProyectil()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Boss"))
        {
            _audio.clip = _clip;
            _audio.Play();
            other.transform.GetComponent<Boss>().hpMin -= 20;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Enemigo"))
        {
            _audio.clip = _clip;
            _audio.Play();
        }
    }

    
}
