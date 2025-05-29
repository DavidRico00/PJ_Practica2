using System;
using UnityEngine;
using TMPro;

public class PlayerGunController : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject armaActualPrefab;
    private Armas armaActual;
    public GameObject[] referenciasArmas;
    public Transform donde;

    private int puntos = 0;

    public TextMeshProUGUI puntosHUD;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void SetGameObjectArma(int indice)
    {
        if (indice == -1)
        {
            if (armaActualPrefab != null)
                Destroy(armaActualPrefab);
            armaActualPrefab = null;
            armaActual = null;
            return;
        }

        if (0 <= indice && indice < referenciasArmas.Length)
        {
            if (armaActualPrefab == referenciasArmas[indice])
                return;

            if (armaActualPrefab != null)
                Destroy(armaActualPrefab);

            armaActualPrefab = Instantiate(referenciasArmas[indice], donde);
            armaActual = armaActualPrefab.GetComponent<Armas>();
        }
    }

    public void Disparar()
    {
        if (armaActual != null && !gameManager.isPaused)
            puntos += armaActual.Shoot();
        actualizarHUD();
    }

    public void Recargar()
    {
        if (armaActual != null && !gameManager.isPaused)
            armaActual.Reload();
    }

    public void SumarMunicion(int municion) {
        if (armaActual != null)
            armaActual.SumarMunicion(municion);
    }

    public void Apuntar()
    {
        if (armaActual != null && !gameManager.isPaused)
            armaActual.Apuntar();
    }

    public void Desapuntar()
    {
        if (armaActual != null && !gameManager.isPaused)
            armaActual.Desapuntar();
    }


    public void DejarDeDisparar()
    {
        if (armaActual != null && !gameManager.isPaused)
            armaActual.setDisparoFalse();
    }


    protected void actualizarHUD()
    {
        puntosHUD.text = String.Format("Puntos: {0}", puntos);
    }

}
