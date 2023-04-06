using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaster : MonoBehaviour
{

    public GameObject _toweradvice;

    private void Start()
    {
        _toweradvice.SetActive(false);
    }

    private void OnMouseOver()
    {
            _toweradvice.SetActive(true);
            Time.timeScale = 0f;
    }

    public void CloseTowerAdvice()
    {
        _toweradvice.SetActive(false);
        Time.timeScale = 1f;
        Destroy(_toweradvice);
    }
}
