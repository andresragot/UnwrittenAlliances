using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;

    protected Transform target;
    public float speed = 50f;
    
    public void Seek(Transform tg)
    {
        target = tg;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.up = new Vector3(dir.x, dir.y, 0);


        //if(dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemigo")
        {
            Destroy(gameObject);
            Enemigo enemigo = target.GetComponent<Enemigo>();
            enemigo.TakeHit(Damage);

        }
    }

    //void HitTarget()
    //{
    //    Destroy(gameObject);
    //    Enemigo enemigo = target.GetComponent<Enemigo>();
    //    enemigo.vida--;
    //    Debug.Log(enemigo.vida);
    //}
}
