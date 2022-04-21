using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    public GameObject ruta;
    private int indice;
    private Vector3 posicion_inicial;
    private Vector3 posicion_siguiente;
    public float velocidad = 1;
    public float distacion_punto = 0.5f;

    private void Start()
    {
        posicion_inicial = this.transform.position;//posicion inicial, donde está primero el enemigo
        posicion_siguiente = ruta.transform.GetChild(0).position;//Los nodos dentro de la carpeta ruta
    }

    void Update()
    {
        Vector3 dir = posicion_siguiente - this.transform.position;
        this.transform.position += dir * velocidad * Time.deltaTime;


        if (dir.magnitude <= distacion_punto)
        {
            if (indice+1 <ruta.transform.childCount)//Si el indice(nº nodos) es menor que el numero total, sigue avanzando
            {
                indice++;
                posicion_siguiente = ruta.transform.GetChild(indice).position;
                Debug.Log("xs" + posicion_siguiente.x + " ys" + posicion_siguiente.y);                
            }
            else//Cuando llega al ultimo nodo vuelve a la posicion inicial
            {
                indice = 0;
                this.transform.position = posicion_inicial;
                posicion_siguiente = ruta.transform.GetChild(0).position;
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.tag == "Bala")
        {
            Destroy(otro.gameObject);
        }
    }
}
