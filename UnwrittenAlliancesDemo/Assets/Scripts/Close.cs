using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{

    public Close()
    {
    }

    private void OnMouseEnter()
    {
        if (MenuControl.MouseInvert)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UpgradeOverlay.Hide_Static();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpgradeOverlay.Hide_Static();
            }
        }
    }
}
