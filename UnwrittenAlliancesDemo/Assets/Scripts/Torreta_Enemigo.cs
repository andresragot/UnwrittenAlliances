using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta_Enemigo : MonoBehaviour
{
    public HumanoPruebaEnemigo human;

    [SerializeField]
    protected Sprite[] sprites;

    protected int spriteIndex = 0;

    protected SpriteRenderer sr;

    public Transform target;

    [Header("Atributos")]
    public float Range = 15f;
    public float fireRate = 1f;
    protected float fireCountdown = 0f;
    protected float Damage = 5;
    //Compra torre
    [SerializeField]
    private int valor_actual;
    private int valor_mejorar;

    public Torreta_Enemigo()
    {
        valor_mejorar = 100;
    }




    [SerializeField]
    protected GameObject bulletPrefab;

    public Transform firePoint;

    public int Valor_actual { get => valor_actual; set => valor_actual = value; }
    public int Valor_mejorar { get => valor_mejorar; set => valor_mejorar = value; }

    Disparar disparar;
    // Start is called before the first frame update
    void Start()
    {
        HUD.ActualizaMoneda(-valor_actual);
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        sr.sprite = sprites[spriteIndex];
        disparar = GetComponentInChildren<Disparar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            disparar.SendDisparo();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    public virtual void Shoot()
    {
        GameObject BulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGO.GetComponent<Bullet>();
        bullet.Damage = Damage;

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    protected void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //if (Vector3.Distance(transform.position, target.position) <= Range)
        //{
        //    return;
        //}
        if (nearestEnemy != null && shortestDistance <= Range)
        {
            if (target != null)
            {
                if(Vector3.Distance(transform.position, target.position) <= Range)
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

    public virtual void Upgrade()
    {
        if (spriteIndex < sprites.Length)
        {
            spriteIndex++;
            Range += 1;
            Damage += 2;
            sr.sprite = sprites[spriteIndex];
            HUD.ActualizaMoneda(-valor_mejorar);
            valor_actual += (int)(valor_mejorar * 0.4f);
            valor_mejorar += (int)(valor_mejorar * 0.4f);
        }
        else
        {
            return;
        }

        
    }

    protected void OnMouseOver()
    {
        UpgradeOverlay.Show_Static(this);
        
    }

    //private void OnMouseExit()
    //{
    //    UpgradeOverlay.Hide_Static();   
    //}

}
