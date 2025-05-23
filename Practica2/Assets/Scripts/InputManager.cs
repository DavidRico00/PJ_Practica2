using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerGunController playerGunController;
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
        inputSuelo.Correr.started += ctx => playerController.Correr();
        inputSuelo.Correr.canceled += ctx => playerController.Correr();
        
        inputSuelo.Recargar.performed += ctx => playerGunController.Recargar();
        inputSuelo.Disparar.started += ctx => IniciarDisparo();
        inputSuelo.Disparar.canceled += ctx => DetenerDisparo();
        inputSuelo.Apuntar.started += ctx => playerGunController.Apuntar();
        inputSuelo.Apuntar.canceled += ctx => playerGunController.Desapuntar();

        inputSuelo.Pause.performed += ctx => gameManager.Pause();
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

    private Coroutine disparoCoroutine;
    private void IniciarDisparo()
    {
        if (disparoCoroutine == null)
            disparoCoroutine = StartCoroutine(DisparoContinuo());
    }

    private void DetenerDisparo()
    {
        if (disparoCoroutine != null)
        {
            StopCoroutine(disparoCoroutine);
            disparoCoroutine = null;
            playerGunController.DejarDeDisparar();
        }
    }

    private IEnumerator DisparoContinuo()
    {
        while (true)
        {
            playerGunController.Disparar();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
