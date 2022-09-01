using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton instancia;
    public static float vida = 100;
    public static float mana = 100;
    public static int vidas = 3;
    public static AudioClip _audio;
    [SerializeField]AudioSource _audSource;
    // Start is called before the first frame update
    void Awake()
    {
        if(Singleton.instancia == null)
        {
            Singleton.instancia = this;
            DontDestroyOnLoad(gameObject);
            _audSource.GetComponent<AudioSource>();
                      
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayAudio(float volume, bool loop)
    {
        instancia._audSource.clip = _audio;
        instancia._audSource.volume = volume;
        instancia._audSource.loop = loop;
        instancia._audSource.Play();
    }

    public static void MenuPrincipal()
    {
        vida = 100;
        mana = 0;
        vidas = 3;
        SceneManager.LoadScene(0);
    }

}
