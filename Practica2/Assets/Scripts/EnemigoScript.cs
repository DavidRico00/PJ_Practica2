using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemigoScript : MonoBehaviour
{
    public float vida;
    public GameObject ammoBoxPrefab;
    public float distJugador;
    private GameObject refugio, jugador;
    private Animator animator;
    public NavMeshAgent navMeshAgent;
    private bool recargandoVida = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        refugio = GameObject.FindWithTag("Refugio");
        jugador = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            float dist = Vector3.Distance(transform.position, jugador.transform.position);
            distJugador = Mathf.Clamp(dist, 0f, 60f);
        }

        if (vida > 0 && !recargandoVida)
        {
            FuzzyDecision(vida, distJugador);
        }

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

    private void FuzzyDecision(float vida, float distJugador)
    {
        float distCerca = pertenenciaTriangular(distJugador, 0f, 5f, 15f);
        float distMedia = pertenenciaTriangular(distJugador, 10f, 15f, 30f);
        float distLejos = pertenenciaTriangular(distJugador, 25f, 40f, 60f);

        float bajaV = pertenenciaTriangular(vida, 0f, 0f, 50f);
        float mediaV = pertenenciaTriangular(vida, 20f, 50f, 80f);
        float altaV = pertenenciaTriangular(vida, 50f, 100f, 100f);

        float retirarse = Mathf.Max(Mathf.Min(distCerca, bajaV), Mathf.Min(distMedia, bajaV));
        float atacar = Mathf.Max(Mathf.Min(distCerca, altaV), Mathf.Min(distCerca, mediaV));
        float moverhaciaJugador = Mathf.Max(Mathf.Min(distMedia, mediaV), Mathf.Min(distLejos, altaV));

        float total = retirarse + atacar + moverhaciaJugador;
        if(total == 0.0f) total = 1f; 

        float valor = (moverhaciaJugador * 0 + retirarse * 50 + atacar * 100) / total;

        //Debug.Log("Valor de decisión: " + valor + ", Retirarse: " + retirarse + ", Atacar: " + atacar + ", Mover al jugador: " + moverhaciaJugador);
        if (valor < 33f)
        {
           MoverAlJugador(); 
        }
        else if (valor < 66f)
        {
            Retirarse(); 
        }
        else
        {
            Atacar();
        }

    }

    private float pertenenciaTriangular(float x, float a, float b, float c)
    {
        if (x < a || x > c)
        {
            return 0f;
        }
        else if (x == b)
        {
            return 1f;
        }
        else if (x < b)
        {
            return (x - a) / (b - a);
        }
        else
        {
            return (c - x) / (c - b);
        }
    }


    private void Atacar()
    {
        Debug.Log("Atacando al jugador");
        //GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            Vector3 direccion = (jugador.transform.position - transform.position);
            direccion.y = 0; // Asegurarse de que la dirección sea horizontal
            if(direccion != Vector3.zero) // Evitar división por cero
            {
                transform.rotation = Quaternion.LookRotation(direccion);
            }
            animator.SetBool("shoot", true);
            animator.SetBool("running", false);
        }

    }

    private void Retirarse()
    {
        navMeshAgent.stoppingDistance = 0.0f;

        Debug.Log("Retirándose del jugador");
        if(refugio == null)  refugio = GameObject.FindWithTag("Refugio");

        if (refugio != null)
        {
            if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled && navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(refugio.transform.position);
            }
            // Vector3 direccion = (refugio.transform.position - transform.position).normalized;
            // transform.rotation = Quaternion.LookRotation(direccion);

            if (animator != null)
            {
                animator.SetBool("running", true);
                animator.SetBool("shoot", false);
            }
            Debug.Log(distJugador + " - Distancia al refugio: " + Vector3.Distance(transform.position, refugio.transform.position));
            if (Vector3.Distance(transform.position, refugio.transform.position) < 2f)
            {
                animator.SetBool("running", false);
                animator.SetBool("shoot", false);
                RegenerarVida(); 
            }
        }
    }

    public float distancia;
    private void MoverAlJugador()
    {
        navMeshAgent.stoppingDistance = distancia; 
        Debug.Log("Moviendo hacia el jugador");
        if(animator != null)
        {
            animator.SetBool("running", true);
            animator.SetBool("shoot", false);   
            Debug.Log("Animación de correr activada.");
        }

        //GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled && navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.destination = jugador.transform.position;

            }
        }
    }


    private void RegenerarVida()
    {
        recargandoVida = true;
        StartCoroutine(HealthRegenCoroutine());
    }

    private IEnumerator HealthRegenCoroutine()
    {
        while (vida < 100f)
        {
            Debug.Log("Regenerando vida: " + vida);
            vida += 10f;
            vida = Mathf.Min(vida, 100f);
            yield return new WaitForSeconds(2f);
        }
        recargandoVida = false;
    }
}
