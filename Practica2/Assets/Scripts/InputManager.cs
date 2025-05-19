using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
    PlayerGunController playerGunController;
    private GameManager gameManager;
    private PlayerInput playerInput;
    public PlayerInput.SueloActions inputSuelo;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerGunController = GetComponent<PlayerGunController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        playerInput = new PlayerInput();
        inputSuelo = playerInput.Suelo;

        inputSuelo.Salto.performed += ctx => playerController.Saltar();
        inputSuelo.Disparar.performed += ctx => playerGunController.Disparar();

        inputSuelo.BOTON_PRUEBAS.performed += ctx => gameManager.BotonPruebas();
    }

    void FixedUpdate()
    {
        playerController.ProcesarMovimiento(inputSuelo.Movimiento.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        playerController.ProcesarCamara(inputSuelo.Mirar.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        inputSuelo.Enable();
    }

    private void OnDisable()
    {
        inputSuelo.Disable();
    }
}
