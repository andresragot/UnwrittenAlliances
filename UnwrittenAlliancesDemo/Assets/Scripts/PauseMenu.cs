using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu Instance;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public static Scene scene;

    private void Start()
    {
        Instance = this;
    }

    public static PauseMenu MyInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<PauseMenu>();
            }
            return Instance;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuMapa");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GanarUno()
    {
        SceneManager.LoadScene("MenuMapa");
        MenuMapa.nivelDos = true;
    }

    public void GanarDos()
    {
        SceneManager.LoadScene("MenuMapa");
        MenuMapa.nivelTres = true;
    }

    public void GanarTres()
    {
        SceneManager.LoadScene("MenuMapa");
        MenuMapa.nivelCuatro = true;
    }

    public void GanarCuatro()
    {
        SceneManager.LoadScene("MenuMapa");
    }

    public void Retry(GameObject ventana)
    {
        Time.timeScale = 1;

        ventana.SetActive(false);
        scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }
}
