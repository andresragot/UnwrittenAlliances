using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private GameObject sucumbicionUI; 

    private AudioSource AS;

    static int vida = 5;

    public static void QuitarVidas(int menos)
    {
        vida -= menos;
    }

    // Start is called before the first frame update
    void Awake()
    {
        sucumbicionUI.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1;
        AS = GetComponent<AudioSource>();
        vida = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            Time.timeScale = 0;
            sucumbicionUI.SetActive(true);
            AS.Stop();
        }
    }
}
