using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreEnanos : MonoBehaviour
{
    public Transform target;

    [Header("Atributos")]
    public float range = 15f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;

    public static int cantidadEnanos = 0;

    [SerializeField]
    GameObject enanoPrefab;

    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0f && cantidadEnanos<4)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject enanoGO = Instantiate(enanoPrefab, firePoint.position, firePoint.rotation);
        HumanoPruebaEnemigo hpe = enanoGO.GetComponent<HumanoPruebaEnemigo>();

        if (hpe != null)
        {
            hpe.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //if (Vector3.Distance(transform.position, target.position) <= range)
        //{
        //    return;
        //}
        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.position) <= range)
                {
                    return;
                }
            }
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}
