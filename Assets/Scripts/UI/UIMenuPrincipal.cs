using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIMenuPrincipal : MonoBehaviour
{
    [SerializeField] GameObject panel = null;
    [SerializeField] GameObject menu = null;
    [SerializeField] bool start = false;
    [SerializeField] Animator anim;
    [SerializeField] AudioClip _audio;
    [SerializeField] float volume;
    [SerializeField] bool looping;

    // Start is called before the first frame update
    void Start() 
    {
        if(panel != null)
        {
            StartCoroutine(IniciarPanel());
        }
        
        Singleton._audio = _audio;
        Singleton.PlayAudio(volume, looping);
    }

    void Update()
    {
        if(!start)
        {
            if(menu != null)
            IniciarMenu();
        }
    }

    public void IniciarMenu()
    {
        if(Input.anyKey)
        {
            menu.SetActive(true);
            panel.SetActive(false);
            start = true;
        }
    }

    public void Jugar()
    {
        StartCoroutine(SiguienteNivel(1));
    }

    // Update is called once per frame
    public void Salir()
    {
        Application.Quit();
    }

    IEnumerator IniciarPanel()
    {
        yield return new WaitForSeconds(3f);

        if(!start)
        panel.SetActive(true);
    }

    public IEnumerator SiguienteNivel(int levelIndex)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(levelIndex);
    }
}
