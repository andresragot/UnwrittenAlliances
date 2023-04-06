using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    Torreta_Enemigo torreta;

    public Upgrade(Torreta_Enemigo torre)
    {
        torreta = torre;
    }

    private void OnMouseEnter()
    {
        if (MenuControl.MouseInvert)
        {
            if (Input.GetMouseButtonDown(1))
            {
                torreta.Upgrade();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                torreta.Upgrade();
            }
        }
    }
}
