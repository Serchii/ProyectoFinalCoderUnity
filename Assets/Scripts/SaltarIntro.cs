using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltarIntro : MonoBehaviour
{
    [SerializeField] bool espacio = false; 
    [SerializeField] GameObject panel;
    [SerializeField] UIMenuPrincipal crossfade;
    [SerializeField] Animation anim = null;
    [SerializeField] bool ending;

    // Start is called before the first frame update
    void Start()
    {
        if(ending)
        {
            StartCoroutine(SiguienteNivel(60,0));
        }
        else
        {
            StartCoroutine(SiguienteNivel(75,2));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!espacio)
        {
            if(Input.anyKey)
            {
                panel.SetActive(true);
                espacio = true;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(ending)
                {
                    StartCoroutine(SiguienteNivel(3,0));
                }
                else
                {
                    StartCoroutine(SiguienteNivel(3,2));
                }
            }
        }
    }
    
    public IEnumerator SiguienteNivel(float seconds,int levelIndex)
    {
        if(anim != null)
        anim.Play();

        yield return new WaitForSeconds(seconds);
        StartCoroutine(crossfade.SiguienteNivel(levelIndex));
    }
}
