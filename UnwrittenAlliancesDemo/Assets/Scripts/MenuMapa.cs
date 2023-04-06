using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMapa : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;

    private static MenuMapa Instance;

    public static bool nivelUno = true, nivelDos = false, nivelTres = false, nivelCuatro = false;

    public static MenuMapa MyInstance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType<MenuMapa>();
            }
            return Instance;
        }
    }

    private void Start()
    {
        ActivateButtons();
    }

    private void ActivateButtons()
    {
        if (nivelUno)
        {
            buttons[0].interactable = true;
        }
        if (nivelDos)
        {
            buttons[1].interactable = true;
        }
        if (nivelTres)
        {
            buttons[2].interactable = true;
        }
        if (nivelCuatro)
        {
            buttons[3].interactable = true;
        }
        if (!nivelDos)
            buttons[1].interactable = false;
        if (!nivelTres)
            buttons[2].interactable = false;
        if (!nivelCuatro)
            buttons[3].interactable = false;
    }

    public void IrNivel(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }
}
