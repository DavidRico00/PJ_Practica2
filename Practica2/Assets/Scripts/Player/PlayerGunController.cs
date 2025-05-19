using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    public GameObject armaActualPrefab;
    public Armas armaActual;
    public GameObject[] referenciasArmas;
    public Transform donde;

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
        if (armaActual != null)
        {
            armaActual.Shoot();
        }
    }   


}
