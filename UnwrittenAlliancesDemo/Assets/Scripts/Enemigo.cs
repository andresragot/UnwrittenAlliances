using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f, damage = 50, timePunch = 0;

    Animator anim;

    public LayerMask mascaraHumano, mascaraEnemigo;

    [SerializeField]
    private int valor_muerte;
    Transform detectar;

    [SerializeField]
    AudioSource[] audios, pisadas;

    enum States { patrol, atacando};

    States status = States.patrol;

    private Transform[] points;
    private Transform target;
    private int wavepointIndex = 0;

    [SerializeField] float vida = 3f, Range = 1f;

    HumanoPruebaEnemigo[] human;


    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.zero;
        target = points[wavepointIndex];
        EnemigoValores.velocidad = speed;

        anim = GetComponent<Animator>();

        InvokeRepeating("EnviarAtacar", 0f, 1f);
        audios[0].Play();
    }
    // Update is called once per frame
    void Update()
    {
        DetectarHumano();

        if(status == States.patrol)
        {
            Mover();
        }

        if (!QuedanHumanos())
        {
            status = States.patrol;
            CambiarVelocidad();
        }

        if (vida <= 0)
        {
            HUD.ActualizaMoneda(valor_muerte);
            Destroy(gameObject);
            WaveSpawner.RestarCantidad();
            audios[1].Play();
        }
    }

    void Mover()
    {
        dir = target.position - transform.position;

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
            GameController.QuitarVidas(1);
            return;
        }
        wavepointIndex++;
        target = points[wavepointIndex];
    }
    public void CambiarVelocidad()
    {
        speed = EnemigoValores.velocidad;
    }
    void Atacar()
    {
        foreach(HumanoPruebaEnemigo hum in human)
        {
            hum.TakeHit(damage);
        }
        audios[2].Play();
        WaitSeconds(timePunch);

    }

    public IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void EnviarAtacar()
    {
        if (status != States.atacando)
        {
            return;
        }
        anim.SetTrigger("Atacando");
    }
    public void TakeHit(float danho)
    {
        vida -= danho;
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
    public void DetectarHumano()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Range, mascaraHumano);
        int index = 0;
        human = new HumanoPruebaEnemigo[hitColliders.Length];
        foreach (Collider2D hit in hitColliders)
        {
            if (hit.tag == "Heroe")
            {
                HumanoPruebaEnemigo humano = hit.gameObject.GetComponent<HumanoPruebaEnemigo>();
                human[index] = humano;
                status = States.atacando;
                index++;
            }
           
        }


        //RaycastHit2D hiter = Physics2D.Raycast(detectar.position, transform.up, 1f, mascaraEnemigo);
        //Debug.DrawRay(detectar.position, transform.up * 1f, Color.blue);

        //if (hiter)
        //{
        //    if (hiter.collider.tag == "Enemigo")
        //    {
        //        Enemigo enem = hiter.collider.gameObject.GetComponent<Enemigo>();
        //        if(enem.speed == 0)
        //        {
        //            speed = 0;
        //        }
        //    }
        //}
    }

    public bool QuedanHumanos()
    {
        bool vuelto = false;
        if (human != null)
        {
            foreach(HumanoPruebaEnemigo h in human)
            {
                vuelto = true;
            }

        }

        return vuelto;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
