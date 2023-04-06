using System.Collections;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ganacionUI;

    [SerializeField]
    GameObject enemyPrefab, spawnPoint, fuertePrefab;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    private bool keyPressed = false;

    public static ArrayList unidades = new ArrayList();

    private static int cantidad = 0, muertos = 0;

    [SerializeField]
    int maximaCantidad = 10, maximoMuertos = 30;

    private void Awake()
    {
        cantidad = 0;
        muertos = 0;
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (muertos >= maximoMuertos)
        {
            Time.timeScale = 0;
            ganacionUI.SetActive(true);
        }

        if (keyPressed)
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave()); // asi se llaman a la funcion de metodo IEnumerator
                countdown = timeBetweenWaves;
            }
            countdown -= Time.deltaTime;
        }

        //cuando el timer se acaba, salen los enemigos
    }

    //metodo que utiliza un timer que permite que los enemigos de la oleada salgan con una distancia de separacion
    IEnumerator SpawnWave()
    {
        float camino = Random.value;
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            if (waveIndex<=5)
            {
                if (camino <= 0.5)
                {
                    SpawnEnemy(1, enemyPrefab);
                }
                else
                {
                    SpawnEnemy(2, enemyPrefab);
                }
            }
            else
            {
                if (waveIndex - i >= waveIndex - 2)
                {
                    if (camino <= 0.5)
                    {
                        SpawnEnemy(1, fuertePrefab);
                    }
                    else
                    {
                        SpawnEnemy(2, fuertePrefab);
                    }
                }
                else
                {
                    if (camino <= 0.5)
                    {
                        SpawnEnemy(1, enemyPrefab);
                    }
                    else
                    {
                        SpawnEnemy(2, enemyPrefab);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Jugar()
    {
        Time.timeScale = 1;
        keyPressed = true;
    }

    //metodo que hace el instantiate para el spawn del enemigo
    void SpawnEnemy(int a, GameObject enemy)
    {
        if (cantidad >= maximaCantidad)
        {
            return;
        }
        else
        {
            GameObject temp = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Enemigo tempEnemigo = temp.GetComponent<Enemigo>();
            tempEnemigo.DefinirCamino(a);

            cantidad++;
            unidades.Add(temp);

        }

    }

    public static void RestarCantidad()
    {
        cantidad--;
        muertos++;
    }

    
}
