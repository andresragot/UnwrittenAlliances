using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMago : Bullet
{
    [SerializeField]
    private float SplashRange;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
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

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemigo")
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);

            foreach(Collider2D vs in hitColliders)
            {
                Enemigo enemy = vs.GetComponent<Enemigo>();
                if (enemy)
                {
                    Vector2 closestPoint = vs.ClosestPoint(transform.position);
                    float distance = Vector3.Distance(closestPoint, transform.position);

                    float damagePercent = Mathf.InverseLerp(SplashRange, 0, distance);
                    enemy.TakeHit(damagePercent * Damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
