using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator anim;
    public Boss boss;
    public int melee;

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
            melee = Random.Range(0,4);

            switch(melee)
            {
                case 0:
                    //Golpe1
                    anim.SetFloat("skills", 0);
                    boss.hitSelect = 0;
                    break;
                case 1:
                    //Golpe2
                    anim.SetFloat("skills", 0.2f);
                    boss.hitSelect = 1;
                    break;
                case 2:
                    //Salto
                    anim.SetFloat("skills", 0.4f);
                    boss.hitSelect = 2;
                    break;
                case 3:
                    //Fireball
                    if(boss.fase == 2)
                    {
                        anim.SetFloat("skills",1);
                    }
                    else
                    {
                        melee = 0;
                    }

                    break;
            }

            anim.SetBool("walk",false);
            anim.SetBool("run",false);
            anim.SetBool("attack",true);
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        
    }
}
