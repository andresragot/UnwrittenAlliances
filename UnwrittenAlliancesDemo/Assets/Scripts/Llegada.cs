using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llegada : MonoBehaviour
{
    [SerializeField]
    AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float valor = Random.value;

        if (valor < 0.25)
        {
            audios[0].Play();
        }
        else if (valor < 0.50)
        {
            audios[1].Play();
        }
        else if (valor < 0.75)
        {
            audios[2].Play();
        }
        else
        {
            audios[3].Play();
        }
    }
}
