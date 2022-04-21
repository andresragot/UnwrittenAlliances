using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints2 : MonoBehaviour
{
    //aca guardamos la pocision de cada uno de los puntos invisibles
    public static Transform[] points2;

    //cuando se inicialice el programa hace esto
    private void Awake()
    {
        //inicializamos el array con la cantidad de hijos que tenemos
        points2 = new Transform[transform.childCount];
        //tenemos el for para rellenar el array de points
        for (int i = 0; i < points2.Length; i++)
        {
            points2[i] = transform.GetChild(i);
        }
    }
}
