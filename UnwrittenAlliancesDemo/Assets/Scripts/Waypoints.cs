using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //aca guardamos la pocision de cada uno de los puntos invisibles
    public static Transform[] points;

    //cuando se inicialice el programa hace esto
    private void Awake()
    {
        //inicializamos el array con la cantidad de hijos que tenemos
        points = new Transform[transform.childCount];
        //tenemos el for para rellenar el array de points
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
