using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    private Transform[] points;
    private Transform target;
    private int wavepointIndex = 0;

    public int vida = 3;

    private bool atacando = false;
    private HumanoPruebaEnemigo human;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        //points = Waypoints.points;
        target = points[wavepointIndex];
        EnemigoValores.velocidad = speed;
    }

    // Update is called once per frame
    void Update()
    {

        Mover();
        if (vida <= 0)
        {
            Destroy(gameObject);
            WaveSpawner.cantidad--;     
        }

        if (atacando)
        {
            StartCoroutine(Atacar());
        }
        if (human != null)
        {
            if (human.vida <= 0)
            {
                CambiarVelocidad();
            }
        }
        
       /* transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            if (wavepointIndex < Waypoints.points.Length - 1)
            {
                wavepointIndex++;
            }
            else
                Destroy(gameObject);
        }*/
    }

    void Mover()
    {
        dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f && speed != 0)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= points.Length)
        {
            Destroy(gameObject);
            WaveSpawner.cantidad--;
            return;
        }
        wavepointIndex++;
        target = points[wavepointIndex];
    }

    public void CambiarVelocidad()
    {
        speed = EnemigoValores.velocidad;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Heroe")
        {
            human = collision.gameObject.GetComponent<HumanoPruebaEnemigo>();
            speed = 0;
            atacando = true;
        }

    }

    IEnumerator Atacar()
    {
        human.vida--;
        yield return new WaitForSeconds(1);
    }

    public void DefinirCamino(int a)
    {
        if (a == 2)
        {
            points = Waypoints2.points2;
        }
        else
        {
            points = Waypoints.points;
        }
    }
}
