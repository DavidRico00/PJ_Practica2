using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public float vida;
    public GameObject ammoBoxPrefab;

    void Start()
    {

    }


    void Update()
    {

    }

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        Debug.Log("Nombre: " + gameObject.name + ", recibiendo daño: " + danio + ", vida actual: " + vida);
        if (vida <= 0)
        {
            Destruir();
        }
    }
    
    private void Destruir()
    {
        float probabilidad = Random.Range(0f, 1f);
        if (probabilidad <= 0.5f && ammoBoxPrefab != null)
        {
            Vector3 posicionSpawn = transform.position + Vector3.up * 0.2f;
            Instantiate(ammoBoxPrefab, posicionSpawn, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
