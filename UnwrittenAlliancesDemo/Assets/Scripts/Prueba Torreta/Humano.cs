using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humano : MonoBehaviour
{

    public GameObject humano;
    public GameObject enemigo;
    public int vida = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D enemigo)
    {
        if (enemigo.gameObject.tag == "enemigo")
        {

            Destroy(enemigo.gameObject);
        }
    }
}
