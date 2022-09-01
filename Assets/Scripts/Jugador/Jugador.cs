using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Jugador : MonoBehaviour
{
    //Variables de movimiento
    public float hor;
    public float ver;
    public float speed = 25f;
    public Animator anim;
    Rigidbody rb;


    //Variables de reinicio
    public float vida = 100;
    public float vidaMax = 100;
    public Image barraHP;
    public float mana = 100;
    public float manaMax = 100;
    public Image barraMana;
    public Text vidas;
    public Image skill;
    bool damage = false;
    bool death = false;
    float timerDamage = 0;
    Vector3 respawnPosition;
    LookAtMouse mira;
    [SerializeField] AudioClip drink;
    [SerializeField] AudioClip noMana;
    [SerializeField] AudioSource _audio;
    [SerializeField] UIMenuPrincipal _menu;

    //Variables para el disparo
    public float timeReload = 0;
    public float reloaded;
    [SerializeField] float timeIce = 0;
    [SerializeField] float reloadIce = 4;
    public GameObject proyectilPrefab;
    public GameObject proyectilPrefabHielo;
    public Transform posicionDisparo;


    // Start is called before the first frame update
    void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        respawnPosition = transform.position; 
        mira = GetComponent<LookAtMouse>();

        vida = Singleton.vida;
        mana = Singleton.mana;
        reloaded = 0.8f;
        timeIce = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Singleton.vida = vida;
        Singleton.mana = mana;
        vidas.text = Singleton.vidas.ToString();
        barraHP.fillAmount = vida / vidaMax;
        barraMana.fillAmount = mana / manaMax;
        skill.fillAmount = timeIce / reloadIce;
        

        if(!death)
        {
            if(Time.timeScale > 0)
            {
                Disparo();
            }
        }
        
        
        if(vida > 100)
        {
            vida = 100;
        }

        if(mana > 100)
        {
            mana = 100;
        }

        if(timeIce < reloadIce)
        {
            timeIce += Time.deltaTime;
        }
        else
        {
            timeIce = reloadIce;
        }

        if(damage)
        {
            timerDamage += Time.deltaTime;
            if(timerDamage > 0.25f)
            {
                EndDamage();
                timerDamage = 0;
            }
        }
        if(death)
        {
            timerDamage += Time.deltaTime;
            if(timerDamage > 5)
            {
                EndDeath();
                timerDamage = 0;
            }
        }

        if(transform.position.y < -10)
        {
            Respawn();
        }
    }

    private void FixedUpdate() 
    {
        MovimientoJugador();
    }

    void MovimientoJugador()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hor,0,ver);

        //transform.position += (new Vector3(hor,0,ver)*speed*Time.deltaTime);
        rb.AddForce (movement * speed/Time.deltaTime);
        anim.SetFloat("PosZ",ver);
        anim.SetFloat("PosX",hor);
    }

    void Disparo()
    {
        if(timeReload < reloaded)
        {
            timeReload += Time.deltaTime;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(proyectilPrefab,posicionDisparo.position,posicionDisparo.rotation);   
                Recarga(false);
            }

            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(mana >= 20 && timeIce >= reloadIce)
                {
                    mana -= 20;
                    Instantiate(proyectilPrefabHielo,posicionDisparo.position,posicionDisparo.rotation);   
                    Recarga(true);  
                }
                else
                {
                    _audio.clip = noMana;
                    _audio.Play();
                }

            }
        }
        
          
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Exit"))
        {
            StartCoroutine(_menu.SiguienteNivel(4));
        }
        
        if(other.CompareTag("ArmaEnemigo"))
        {
            vida -= 15;
            Damage();
        }

        if(other.CompareTag("Potion"))
        {
            _audio.clip = drink;
            _audio.Play();
            if(other.transform.name.Substring(0,8) == "HPPotion")
            {
                vida += 50;
                Destroy(other.gameObject);
            }

            if(other.transform.name.Substring(0,10) == "ManaPotion")
            {
                mana += 25;
                Destroy(other.gameObject);
            }
        }

        if(other.CompareTag("Crystal"))
        {
            StartCoroutine(_menu.SiguienteNivel(5));
        }

        
    }

    void Respawn()
    {
        
        transform.position = respawnPosition;
        vida = 100;
        mana = 100;
        speed = 20;
        mira.enabled = true;
           
    }

    void Recarga(bool ice)
    {
        if(ice)
        {
            timeIce = 0;
        }
        timeReload = 0;
    }

    public void Damage()
    {

        if(vida > 0)
        {
            anim.SetBool("damage",true);
            damage = true;
        }
        else
        {
            anim.SetBool("death",true);
            death = true;
            speed = 0;
            mira.enabled = false;
        }

    }
    public void EndDamage()
    {
        anim.SetBool("damage",false);
        damage = false;
    }

    public void EndDeath()
    {
        Singleton.vidas --;

        if(Singleton.vidas > 0)
        {
            anim.SetBool("death",false);
            death = false;
            Respawn();
        }
        else
        {
            StartCoroutine(_menu.SiguienteNivel(6));
        }
        
    }

}
