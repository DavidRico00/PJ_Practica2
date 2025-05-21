using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject armaActualPrefab;
    private Armas armaActual;
    public GameObject[] referenciasArmas;
    public Transform donde;

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
            armaActual.Shoot();
    }

    public void Recargar()
    {
        if (armaActual != null && !gameManager.isPaused)
            armaActual.Reload();
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
}
