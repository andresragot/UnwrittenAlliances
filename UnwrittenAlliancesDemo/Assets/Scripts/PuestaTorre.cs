using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuestaTorre : MonoBehaviour
{
    [SerializeField]
    RectTransform m_parent;

    public PonerTorre poner;

    RectTransform rt;

    [SerializeField]
    GameObject[] torres;

    [SerializeField]
    Button[] buttons;

    private static PuestaTorre Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        rt = GetComponent<RectTransform>();
        Hide();
    }

    public static void Show_Static(PonerTorre ponerTorre)
    {
        Instance.Show(ponerTorre);
    } 

    public static void Hide_Static()
    {
        Instance.Hide();
    }

    private void Show(PonerTorre ponerTorre)
    {
        gameObject.SetActive(true);
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, Camera.main.WorldToScreenPoint(ponerTorre.transform.position), Camera.current, out anchoredPosition);
        //rt.anchoredPosition = Camera.main.WorldToScreenPoint(anchoredPosition);
        rt.anchoredPosition = anchoredPosition;
        poner = ponerTorre;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarBotones();
    }

    void ActualizarBotones()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Torreta_Enemigo te = torres[i].gameObject.GetComponent<Torreta_Enemigo>();
            if(te.Valor_actual >= HUD.Contador_monedas)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void PonerTorre(GameObject torre)
    {
        GameObject clon = Instantiate(torre);
        clon.transform.position = poner.gameObject.transform.position;
        Destroy(poner.gameObject);

        Hide();
    }

    public void Close()
    {
        Hide();
    }

}
