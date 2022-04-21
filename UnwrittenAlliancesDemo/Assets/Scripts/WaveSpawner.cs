using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab, spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    public static ArrayList unidades = new ArrayList();

    public static int cantidad = 0;

    // Update is called once per frame
    void Update()
    {
        //cuando el timer se acaba, salen los enemigos
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); // asi se llaman a la funcion de metodo IEnumerator
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    //metodo que utiliza un timer que permite que los enemigos de la oleada salgan con una distancia de separacion
    IEnumerator SpawnWave()
    {
        float puerta = Random.value;
        Debug.Log(puerta);
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            if (puerta <= 0.5)
            {
                SpawnEnemy(1);
            }
            else
            {
                SpawnEnemy(2);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //metodo que hace el instantiate para el spawn del enemigo
    void SpawnEnemy(int a)
    {
        if (cantidad >= 12)
        {
            return;
        }
        else
        {
            Debug.Log(a);
            GameObject temp = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Enemigo tempEnemigo = temp.GetComponent<Enemigo>();
            tempEnemigo.DefinirCamino(a);

            cantidad++;
            unidades.Add(temp);

        }

    }
}
