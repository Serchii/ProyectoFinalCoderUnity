using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJuego : MonoBehaviour
{
    public LookAtMouse lookPlayer;
    [SerializeField] private bool pauseActive = false;
    
    public GameObject pauseMenu;
    
    public GameObject camPausa;
    GameObject camRotation;

    public static GameObject camaraActiva;

    // Start is called before the first frame update
    void Start()
    {
        camRotation = camPausa.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TogglePause();



        if(pauseActive)
        {
            CamPausa();
        }
    }

    void TogglePause()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseActive) //unpause
            {
                pauseMenu.SetActive(false);
                pauseActive = false;
                Time.timeScale = 1;
                lookPlayer.enabled = true;
                camPausa.SetActive(false);
                camRotation.SetActive(false);
                camaraActiva.SetActive(true);
            }
            else //pause
            {
                pauseMenu.SetActive(true);
                pauseActive = true;
                Time.timeScale = 0;
                lookPlayer.enabled = false;
                camPausa.SetActive(true);
                camRotation.SetActive(true);
                camaraActiva.SetActive(false);
            }
        }
    }



    void CamPausa()
    {
        float speedRotation = 5;
        camPausa.transform.Rotate(0,speedRotation*Time.unscaledDeltaTime,0);
    }

}
