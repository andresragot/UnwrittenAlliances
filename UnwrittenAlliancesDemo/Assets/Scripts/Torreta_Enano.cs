using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta_Enano : Torreta_Enemigo
{

    // Start is called before the first frame update
    void Start()
    {
        HUD.ActualizaMoneda(-Valor_actual);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        if (!human)
        {
            GameObject BulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            human = BulletGO.GetComponent<HumanoPruebaEnemigo>();
            human.damage = Damage;
        }       
    }

    public override void Upgrade()
    {
        if (spriteIndex < sprites.Length)
        {
            HUD.ActualizaMoneda(-Valor_mejorar);
            spriteIndex++;
            Range += 10;
            Damage += 2;
            sr.sprite = sprites[spriteIndex];
            Valor_actual += (int)(Valor_mejorar * 0.4);
            Valor_mejorar += (int)(Valor_mejorar * 0.4);
            human.damage = Damage;

        }
        else
        {
            return;
        }


    }

    

}
