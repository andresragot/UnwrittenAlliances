using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonerTorre : MonoBehaviour
{

    public GameObject torre;
    private void OnMouseDown()
    {
        GameObject temp;//temporal
        Vector3 pos = this.transform.position;//guardar posicion target
        pos.y = pos.y + 0.4f;//Para que la torre se coloque mas arriba y cuadre mejor
        temp = Instantiate(torre);//para crear un objeto ya creado
        temp.transform.position = pos;//ponerlo en la posicion guardada
        //temp.layer = 5; //Seria para cambiar el numero de layer al ponerla
        Destroy(this.gameObject);//Destroir el target al pulsar y colocar la torre
    }
}
