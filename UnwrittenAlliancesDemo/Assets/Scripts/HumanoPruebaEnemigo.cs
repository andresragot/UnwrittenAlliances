using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoPruebaEnemigo : MonoBehaviour
{
    Animator anim;

    enum States { patrol, pursuit }

    States status = States.patrol;

    public float vida = 500, damage = 1, Range = 2f, timePunch = 1f;

    private Enemigo[] enemigos;
    public const int VIDA_MAXIMA = 3;



    public LayerMask mascaraEnemigo;

    private Transform target;
    
    [SerializeField]
    int speed = 1;
    

    public int waypointIndex;

    [SerializeField]
    AudioSource[] audios, pisadas;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    private void Start()
    {
        GetNearWaypoint();
        InvokeRepeating("EnviarAtacar", 0f, 1f);
        anim = GetComponent<Animator>();
        audios[0].Play();
    }
    private void Update()
    {
        DetectarEnemigo();

      
        if (status == States.patrol)
        {
            Mover();
        }
        if (vida <= 0)
        {
            audios[1].Play();
            new WaitForSeconds(audios[1].time);
            Destroy(gameObject);
        }
    }

    private void EnviarAtacar()
    {
        if(status != States.pursuit)
        {
            return;
        }
        anim.SetTrigger("ataque");
    }
    
    void Mover()
    {
        Vector3 distanciaWP=Vector3.zero;
        if (waypointIndex != -1)
        {

            anim.SetBool("Patrol", true);
            Vector3 dir = target.position -transform.position;


            //transform.up = new Vector3(dir.x, dir.y, 0);

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            float random = Random.value;
            if (random < 0.25)
                pisadas[0].Play();
            else if (random < 0.50)
            {
                pisadas[1].Play();
            }
            else if (random < 0.75)
            {
                pisadas[2].Play();
            }
            else
            {
                pisadas[3].Play();
            }
        }
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            anim.SetBool("Patrol", false);
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
            float distanceToWP = Vector3.Distance(transform.position, tf.position);
            if (distanceToWP < shortestDistance)
            {
                shortestDistance = distanceToWP;
                shortestWaypoint = tf;
            }
        }

        foreach(Transform ft in Waypoints2.points2)
        {
            float distancToWP = Vector3.Distance(transform.position, ft.position);
            if(distancToWP < shortestDistance)
            {
                shortestDistance = distancToWP;
                shortestWaypoint = ft;
            }
        }
        target = shortestWaypoint;
        waypointIndex = System.Array.IndexOf(Waypoints.points, target);
        if(waypointIndex == -1)
        {
            waypointIndex = System.Array.IndexOf(Waypoints2.points2, target);
        }
    }

    public void Seek(Transform transform)
    {

    }

    private void Atacar()
    {
        foreach(Enemigo enemigo in enemigos)
        {
            enemigo.TakeHit(damage);
        }

        audios[3].Play();
    }

    public IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void TakeHit(float danho)
    {
        vida -= danho;
    }

    public void DetectarEnemigo()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Range, mascaraEnemigo);
        int index = 0;
        enemigos = new Enemigo[hitColliders.Length];
        foreach(Collider2D hit in hitColliders)
        {
            if(hit.tag == "Enemigo")
            {
                Enemigo enemigo = hit.gameObject.GetComponent<Enemigo>();
                enemigos[index] = enemigo;
                status = States.pursuit;
                index++;
            }
        }
        if(index == 0)
        {
            status = States.patrol;
        }
    }

}
