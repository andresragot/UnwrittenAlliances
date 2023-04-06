using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private static HUD instancia;
    [SerializeField]
    private Text monedas;
    private static int contador_monedas = 200;
    [SerializeField]
    public AudioSource coin;

    public static int Contador_monedas 
    { get => contador_monedas; set => contador_monedas = value; }

    public static void ActualizaMoneda(int valor)
    {
        Contador_monedas += valor;
       instancia.SonidoMoneda();

    }

    public void SonidoMoneda()
    {
        coin.Play();
    }

    public static HUD GetInstance()
    {
        return instancia;
    }

    private void Start()
    {
        Contador_monedas = 200;
        coin = GetComponentInChildren<AudioSource>();
        instancia = this;
    }

    void Update()
    {
        monedas.text = Contador_monedas.ToString();
    }
}
