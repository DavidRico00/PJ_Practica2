using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controlador;
    private InputManager inputManager;
    private Vector3 pVelocidad;
    private bool isGrounded;
    private float xRotacion = 0f, velocidad = 5f, gravedad = -9.81f, fuerzaSalto = 1f;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private TextMeshProUGUI promptText;

    public Camera camara;
    public float sensibilidadX, sensibilidadY;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        sensibilidadX = PlayerPrefs.GetFloat("SensibilidadX", 30f);
        sensibilidadY = PlayerPrefs.GetFloat("SensibilidadY", 30f);
    }

    void Update()
    {
        promptText.text = string.Empty;
        isGrounded = controlador.isGrounded;

        Ray ray = new Ray(camara.transform.position, camara.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.5f))
            if (hit.collider.GetComponent<Interactuable>() != null)
            {
                Interactuable interactuable = hit.collider.GetComponent<Interactuable>();
                promptText.text = interactuable.promptText;
                if (inputManager.inputSuelo.Interactuar.triggered)
                    interactuable.BaseInteract();
            }
    }

    public void ProcesarMovimiento(Vector2 input)
    {
        Vector3 movimiento = Vector3.zero;
        movimiento.x = input.x;
        movimiento.z = input.y;
        controlador.Move(transform.TransformDirection(movimiento) * velocidad * Time.deltaTime);
        pVelocidad.y += gravedad * Time.deltaTime;
        if (isGrounded && pVelocidad.y < 0)
            pVelocidad.y = -2f;
        controlador.Move(pVelocidad * Time.deltaTime);
    }

    public void ProcesarCamara(Vector2 input)
    {
        xRotacion -= (input.y * Time.deltaTime) * sensibilidadY;
        xRotacion = Mathf.Clamp(xRotacion, -80f, 80f);

        camara.transform.localRotation = Quaternion.Euler(xRotacion, 0f, 0f);
        transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * sensibilidadX);
    }

    public void Saltar()
    {
        if (isGrounded)
            pVelocidad.y = Mathf.Sqrt(fuerzaSalto * -3f * gravedad);
    }

    public void Correr()
    {
        if (velocidad == 5f)
            velocidad = 8f;
        else
            velocidad = 5f;
    }

    public void CambiarSensibilidad()
    {
        sensibilidadX = PlayerPrefs.GetFloat("SensibilidadX", 30f);
        sensibilidadY = PlayerPrefs.GetFloat("SensibilidadY", 30f);
    }
}
