using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    [SerializeField]
    Torreta_Enemigo te;

    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        
        te.GetComponentInParent<Torreta_Enemigo>();
        anim.GetComponent<Animator>();
    }
    public void SendDisparo()
    {
        anim.SetTrigger("ataque");
    }
    public void Disparo()
    {
        te.Shoot();
    }
}
