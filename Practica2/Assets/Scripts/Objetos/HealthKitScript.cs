using UnityEngine;

public class HealthKitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerHealth jugador = collider.GetComponent<PlayerHealth>();
            if (jugador != null)
                jugador.AumentarBotiquin();

            Destroy(gameObject);
        }
    }
}
