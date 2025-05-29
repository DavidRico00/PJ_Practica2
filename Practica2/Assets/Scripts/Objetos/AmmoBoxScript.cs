using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{
    public int municion;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerGunController jugador = collider.GetComponent<PlayerGunController>();
            if (jugador != null)
                jugador.SumarMunicion(municion);

            Destroy(gameObject);
        }
    }
}
