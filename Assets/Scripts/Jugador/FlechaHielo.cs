using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaHielo : Flecha
{
    [SerializeField]Renderer flechaRenderer;
    public GameObject escarcha;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoProyectil();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemigo"))
        {
            /*_audio.clip = _clip;
            _audio.Play();
            collision.transform.GetComponent<Enemigo>().speed = 0.5f;
            Instantiate(escarcha,transform.position,transform.rotation);
            collision.transform.GetComponent<Enemigo>().Damage();
            Destroy(gameObject);*/
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Boss"))
        {
            _audio.clip = _clip;
            _audio.Play();
            other.transform.GetComponent<Boss>().speed = 2 * 0.5f;
            Instantiate(escarcha,transform.position,transform.rotation);
            other.transform.GetComponent<Boss>().hpMin -= 20;
            Destroy(gameObject);
        }
    }
}
