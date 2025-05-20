using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public float vida;

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
        Destroy(gameObject);
    }
}
