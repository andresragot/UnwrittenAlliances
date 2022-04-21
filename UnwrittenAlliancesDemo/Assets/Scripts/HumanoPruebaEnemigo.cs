using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoPruebaEnemigo : MonoBehaviour
{
    enum States { patrol, pursuit }

    States state = States.patrol;

    public int vida = 500;

    private bool atacando = false;
    private Enemigo enemigo;
    public const int VIDA_MAXIMA = 3;

    private Transform target;
    //private int wavepointIndex = 0;
    //[SerializeField]
    int speed = 3;
    [SerializeField]
    Transform spawn;

    public int waypointIndex;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        //Gizmos.DrawWireSphere(target.position, 1f);
    }

    private void Start()
    {
        GetNearWaypoint();
    }
    private void Update()
    {
        var ob = Physics2D.CircleCast(transform.position, 1f, Vector2.up);
        if (state == States.pursuit)
        {
            target = ob.collider.transform;
            if (Vector3.Distance(target.position, transform.position) > 1f)
            {
                target = transform;
                state = States.patrol;
                return;
            }
        }
        else if (state == States.patrol)
        {
            Mover();
            if (ob.collider != null)
            {
                if (ob.collider.CompareTag("Enemigo"))
                {
                    state = States.pursuit;
                    return;
                }
            }
        }
        //  Mover();
        if (vida <= 0)
        {
            Destroy(gameObject);
            atacando = false;
        }
        if (atacando)
        {
            StartCoroutine(Atacar());
        }
        if (enemigo != null)
        {
            if (enemigo.vida <= 0)
            {
                atacando = false;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemigo")
        {
            atacando = true;
            enemigo = collision.gameObject.GetComponent<Enemigo>();
            if (enemigo.vida <= 0)
            {
                atacando = false;
                //while (vida < VIDA_MAXIMA)
                //{
                //    vida++;

                //}
            }
        }
    }


    void Mover()
    {
        Vector3 distanciaWP=Vector3.zero;
        if (waypointIndex != -1)
        {
            distanciaWP = Waypoints.points[waypointIndex - 1].position;
            Vector3 dir = target.position - distanciaWP;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
        if (Vector3.Distance(transform.position, distanciaWP) <= 1f /*&& speed != 0*/)
        {
            speed = 0;
            waypointIndex = -1;
        }
    }


    void GetNearWaypoint()
    {
        float shortestDistance = Mathf.Infinity;
        Transform shortestWaypoint = null;
        foreach(Transform tf in Waypoints.points)
        {
            float distanceToWP = Vector3.Distance(spawn.position, tf.position);
            if (distanceToWP < shortestDistance)
            {
                shortestDistance = distanceToWP;
                shortestWaypoint = tf;
            }
        }
        target = shortestWaypoint;
        waypointIndex = System.Array.IndexOf(Waypoints.points, target);



        //if (wavepointIndex >= Waypoints.points.Length)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //wavepointIndex++;
        //target = Waypoints.points[wavepointIndex];
    }

    public void Seek(Transform transform)
    {

    }

    IEnumerator Atacar()
    {
        enemigo.vida--;
        yield return new WaitForSeconds(0.5f);
    }
}
