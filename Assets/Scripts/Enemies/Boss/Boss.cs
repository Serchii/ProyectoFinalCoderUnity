using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Atributos Boss")]
    public int rutina;
    public float cronometro;
    public float timeRutinas;
    public Animator anim;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando = false;
    public RangoBoss rango;
    public float speed;
    [SerializeField] float contSpeed;
    [SerializeField] float speedMax;
    public GameObject[] hit;
    public int hitSelect;
    [SerializeField] ParticleSystem _escombro;
    [SerializeField]SkinnedMeshRenderer mr;

    [Header("Lanzallamas")]
    public bool lanzallamas;
    public List<GameObject> pool = new List<GameObject>();
    public GameObject fire;
    public GameObject cabeza;
    private float cronometro2;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _clip;
    

    [Header("Jump Attack")]
    public float jumpDistance;
    public bool directionSkill;

    [Header("Fireball")]
    public GameObject fireball;
    public GameObject point;
    public List<GameObject> pool2 = new List<GameObject>();

    public int fase = 1;
    public float hpMin;
    public float hpMax;
    public Image barra;
    //AudioSource
    public bool death;
    [SerializeField] GameObject spawnItem;
    [SerializeField] GameObject crystal;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        _audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        _audio.clip = _clip;
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        barra.fillAmount = hpMin / hpMax;

        if(hpMin > 0)
        {
            Vivo();
        }
        else
        {
            if(!death)
            {
                anim.SetTrigger("dead");
                //Fin Musica
                death = true;
            }
        }

        if(speed < 2)
        {
            contSpeed += Time.deltaTime;
            mr.material.color = Color.cyan;

            if(contSpeed > 4)
            {
                contSpeed = 0;
                speed = speedMax;
                mr.material.color = Color.white;
            }
        }
    }

    public void ComportamientoBoss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 30)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            point.transform.LookAt(target.transform.position);
            //Musica

            if(Vector3.Distance(transform.position, target.transform.position) > 2 && !atacando)
            {
                switch(rutina)
                {
                    case 0: //Walk
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("walk", true);
                        anim.SetBool("run", false);

                        if(transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }

                        anim.SetBool("attack",false);

                        cronometro += 1 * Time.deltaTime;
                        
                        if(cronometro > timeRutinas)
                        {
                            rutina = Random.Range(0,5);
                            cronometro = 0;
                        }

                        break;
                    case 1: //Run
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("walk", false);
                        anim.SetBool("run", true);

                        if(transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed*2 * Time.deltaTime);
                        }

                        anim.SetBool("attack",false);
                        break;
                    case 2: //Lanzallamas
                        anim.SetBool("walk", false);
                        anim.SetBool("run", false);
                        anim.SetBool("attack",true);
                        anim.SetFloat("skills", 0.8f);

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                    case 3:
                        if(fase == 2)
                        {
                            jumpDistance += 1*Time.deltaTime;
                            anim.SetBool("walk", false);
                            anim.SetBool("run", false);
                            anim.SetBool("attack",true);
                            anim.SetFloat("skills", 0.6f);
                            hitSelect = 3;
                            rango.GetComponent<CapsuleCollider>().enabled = false;

                            if(directionSkill)
                            {
                                if(jumpDistance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }

                                transform.Translate(Vector3.forward * 16 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }

                        break;
                    case 4: //Fireball
                        if(fase == 2)
                        {
                            anim.SetBool("walk", false);
                            anim.SetBool("run", false);
                            anim.SetBool("attack",true);
                            anim.SetFloat("skills", 1);
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
    
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }

                        break;
                }
            }
        }
    }

    public void FinalAnim()
    {
        rutina = 0;
        anim.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        lanzallamas = false;
        jumpDistance = 0;
        directionSkill = false;
    }

    public void DirectionAttackStart()
    {
        directionSkill = true;
    }

    public void DirectionAttackFinal()
    {
        directionSkill = false;
    }

    public void ColliderWeaponTrue()
    {
        hit[hitSelect].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[hitSelect].GetComponent<SphereCollider>().enabled = false;
    }

    public GameObject GetBola()
    {
        GameObject obj;

        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        obj = Instantiate(fire,cabeza.transform.position,cabeza.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }

    public void LanzallamasSkill()
    {
        GameObject obj;

        cronometro2 += 1*Time.deltaTime;
        if(cronometro2 > 0.1f)
        {
            obj = GetBola();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            cronometro2 = 0;
        }
    }

    public void StartFire()
    {
        lanzallamas = true;
        _particle.Play();
    }

    public void StopFire()
    {
        lanzallamas = false;
        _particle.Stop();
    }

    public GameObject GetFireball()
    {
        GameObject obj;

        for (int i = 0; i < pool2.Count; i++)
        {
            if(!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }

        obj = Instantiate(fireball,point.transform.position,point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }

    public void FireballSkill()
    {
        GameObject obj = GetFireball();
        obj.transform.position = point.transform.position;
        obj.transform.rotation = point.transform.rotation;
    }

    public void Vivo()
    {
        if(hpMin < 1200)
        {
            fase = 2;
            timeRutinas = 1;
        }

        ComportamientoBoss();

        if(lanzallamas)
        {
            LanzallamasSkill();
        }
    }

    public void PlayEscombro()
    {
        _escombro.Play();
    }

    public void EndDeath()
    {
        Instantiate(crystal,new Vector3(transform.position.x,1,transform.position.z),transform.rotation);
        Destroy(gameObject,2f);
    }
}
