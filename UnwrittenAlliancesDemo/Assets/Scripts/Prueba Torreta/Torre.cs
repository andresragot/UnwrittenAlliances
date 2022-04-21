using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{
    public GameObject enemigo;
    public float distancia_umbral = 2;//distancia disparo a enemigo
    private float siguienteDisparo;
    public float tiempoEntreDisparo = 1f;
    void Start()
    {
        siguienteDisparo = Time.time;
    }
   

    // Update is called once per frame
    void Update()
    {
        enemigo = BuscarEnemigoCercano();
        if (Time.time>=siguienteDisparo)
        {
            if (enemigo != null)
            {
                Disparar();
                Debug.DrawLine(this.transform.position, enemigo.transform.position, Color.yellow);//Dibujar laser de guia
                siguienteDisparo = Time.time + tiempoEntreDisparo;//tiempo de espera entre disparos
            }
        }
        
        //float dist = (enemigo.transform.position - this.transform.position).magnitude;

        //if (dist <= distancia_umbral)
        //{
        //    Debug.DrawLine(this.transform.position, enemigo.transform.position, Color.green);//Linea para ver la distancia al enemigo(solo se ve en la scene
        //}

    }

    GameObject BuscarEnemigoCercano()
    {
        ArrayList enemigos = WaveSpawner.unidades;
        GameObject temp;
        foreach (Object item in enemigos)
        {
            temp = (GameObject)item;
            if (Vector3.Distance(temp.transform.position, this.transform.position) < distancia_umbral)
            {
                Debug.Log("a");
                return temp;
            }
        }
        return null;
    }

    void Disparar()//clase disparar
    {
        GameObject obj = (GameObject)Instantiate(GameObject.Find("Bala"), this.transform.position, Quaternion.identity);
        Bala bala = obj.GetComponent<Bala>();
        bala.ActivarBala(this);
    }

}
