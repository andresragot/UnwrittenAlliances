using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Esperar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.UnloadSceneAsync(PauseMenu.scene);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Empezar()
    {
        SceneManager.LoadScene(PauseMenu.scene.buildIndex);
    }

  

}
