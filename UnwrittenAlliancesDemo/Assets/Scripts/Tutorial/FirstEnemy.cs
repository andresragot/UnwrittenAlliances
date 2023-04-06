using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    public GameObject _enemyadvice;

    private void Start()
    {
        _enemyadvice.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemyadvice.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseAdvice()
    {
        _enemyadvice.SetActive(false);
        Time.timeScale = 1f;
        Destroy(gameObject);

    }
}
