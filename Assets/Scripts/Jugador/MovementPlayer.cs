using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public int hor = 0;
    public int ver = 0;
    Rigidbody rb;
    [SerializeField] float speed = 20f;
    [SerializeField] ParticleSystem shoot;
    [SerializeField] Vector3 reinicio;
    [SerializeField] bool blue = true;
    //[SerializeField] float speedRotation = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reinicio = transform.position;
        ClaseEvento.disparo += Disparo;
        ClaseEvento.reinicio += Reiniciar;
        ClaseEvento.cambioColor += CambiarColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        PlayerMovementX(hor);
        PlayerMovementY(ver);
    }

    public void PlayerMovementX(int hor)
    {
        Vector3 movement = new Vector3(hor,0,0);

        rb.AddForce(movement*speed/Time.deltaTime);
    }
    public void PlayerMovementY(int ver)
    {
        Vector3 movement = new Vector3(0,0,ver);

        rb.AddForce(movement*speed/Time.deltaTime);
    }

    void Disparo()
    {
        shoot.Play();
    }

    void Reiniciar()
    {
        transform.position = reinicio;
    }

    void CambiarColor()
    {
        if(blue)
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            blue = false;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
            blue = true;
        }
        
    }

    void OnDisable()
    {
        ClaseEvento.disparo -= Disparo;
        ClaseEvento.reinicio -= Reiniciar;
        ClaseEvento.cambioColor -= CambiarColor;
    }
}
