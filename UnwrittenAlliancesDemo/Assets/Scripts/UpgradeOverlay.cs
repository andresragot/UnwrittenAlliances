using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeOverlay : MonoBehaviour
{

    [SerializeField]
    RectTransform m_parent;

    [SerializeField]
    GameObject prefabTarget;

    [SerializeField]
    Button upgradeBtn;

    public Torreta_Enemigo torre;

    RectTransform rt;

    private static UpgradeOverlay Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        rt = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        ActualizarBotonUpgrade();
    }

    public static void Show_Static(Torreta_Enemigo torreta)
    {
        Instance.Show(torreta);
    }

    public static void Hide_Static()
    {
        Instance.Hide();
    }

    private void Show(Torreta_Enemigo torre)
    {
        gameObject.SetActive(true);
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, Camera.main.WorldToScreenPoint(torre.transform.position), Camera.current, out anchoredPosition);
        //rt.anchoredPosition = Camera.main.WorldToScreenPoint(anchoredPosition);
        rt.anchoredPosition = anchoredPosition;
        this.torre = torre;               
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void Upgrade()
    {
        torre.Upgrade();
    }

    void ActualizarBotonUpgrade()
    {
        if (torre.Valor_mejorar >= HUD.Contador_monedas)
        {
            upgradeBtn.interactable = false;
        }
        else
            upgradeBtn.interactable = true;
    }

    public void Destroy()
    {
        GameObject cln = Instantiate(prefabTarget);
        cln.transform.position = torre.gameObject.transform.position;
        if (torre.human)
        {
            torre.human.vida = 0;
        }
        HUD.ActualizaMoneda(torre.Valor_actual);
        Destroy(torre.gameObject);

        Hide();


    }
}
