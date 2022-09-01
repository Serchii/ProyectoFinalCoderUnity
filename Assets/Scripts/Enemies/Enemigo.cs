using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class Enemigo : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator anim;
    public Quaternion angulo;
    public float speed = 1f;
    float contSpeed = 0;
    public float grado;
    [SerializeField] int vida = 100;
    bool damage = false;
    bool death = false;
    [SerializeField]Rigidbody rb;
    [SerializeField]Collider col;
    [SerializeField]SkinnedMeshRenderer mr;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject hpPotion;
    [SerializeField] GameObject mpPotion;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip[] deaths;

    GameObject target;
    public bool atacando;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        _audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!death)
        {
            ComportamientoEnemigo();
        }
        

        if(speed < 1)
        {
            contSpeed += Time.deltaTime;

            if(contSpeed > 4)
            {
                contSpeed = 0;
                speed = 1;
                
                mr.material.color = Color.white;
            }
        }
    }

    public void ComportamientoEnemigo()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > 10)
        {
            anim.SetBool("run",false);
            
            cronometro += 1*Time.deltaTime;

            if(cronometro >= 4)
            {
                rutina = Random.Range(0,2);
                cronometro = 0;
            }

            switch(rutina)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0,360);
                    angulo = Quaternion.Euler(0,grado,0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1*speed * Time.deltaTime);
                    anim.SetBool("walk", true);
                    break;   
              
            }
        }
        else
        {

            if(Vector3.Distance(transform.position, target.transform.position) > 1.5 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                anim.SetBool("walk", false);

                anim.SetBool("run",true);
                transform.Translate(Vector3.forward * 4*speed * Time.deltaTime);
                anim.SetBool("attack",false);
            }
            else
            {
                anim.SetBool("run",false);
                anim.SetBool("walk",false);

                anim.SetBool("attack",true);
                atacando = true;
            }
        }
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Flecha"))
        {
            if(other.transform.name.Substring(0,6) == "Flecha")
            {
                other.GetComponent<Flecha>()._audio.clip = other.GetComponent<Flecha>()._clip;
                other.GetComponent<Flecha>()._audio.Play();
            }
            if(other.transform.name.Substring(0,11) == "FlechaHielo")
            {
                other.GetComponent<FlechaHielo>()._audio.clip = other.GetComponent<FlechaHielo>()._clip;
                other.GetComponent<FlechaHielo>()._audio.Play();
                GetComponent<Enemigo>().speed = 0.5f;
                mr.material.color = Color.cyan;
                Instantiate(other.GetComponent<FlechaHielo>().escarcha,other.transform.position,other.transform.rotation);
            }
            Damage();
            Destroy(other.gameObject);
        }
    }

    public void Damage()
    {
        vida -= 20;

        if(vida <= 0)
        {
            if(!death)
            {
                int soundDead = Random.Range(0,deaths.Length);
                _audio.clip = deaths[soundDead];
                _audio.Play();
            }
            anim.SetBool("death",true);
            death = true;
            anim.SetBool("run",false);
            anim.SetBool("walk",false);
            anim.SetBool("attack",false);
            rb.isKinematic = true;
            col.isTrigger = true;
            
        }
        else
        {
            anim.SetBool("damage",true);
            damage = true;
        }

    }
    public void EndDamage()
    {
        anim.SetBool("damage",false);
        damage = false;
    }

    public void EndDeath()
    {
        float item = Random.Range(0,10);

        if(item > 8.5)
        {
            Instantiate(hpPotion,spawn.transform.position,spawn.transform.rotation);
        }
        else if (item > 6)
        {
            Instantiate(mpPotion,spawn.transform.position,spawn.transform.rotation);
        }

        Destroy(gameObject);
    }

    public void FinalAnim()
    {
        anim.SetBool("attack", false);
        atacando = false;
    }
}


