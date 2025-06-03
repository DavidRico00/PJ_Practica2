using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{

    private float vida, lerpTimer;
    public float vidaMaxima = 100f, chipSpeed = 2f;
    public Image frontHealthBar, backHealthBar;
    private int numBotiquines = 2;

    public TextMeshProUGUI numeroBotiquinesHUD;

    private AudioSource audioSource;
    public AudioClip muerteS;

    void Start()
    {
        vida = vidaMaxima;
        ActualizarHUDBotiquines();
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        vida = Mathf.Clamp(vida, 0f, vidaMaxima);
        ActualizarUIVida();
    }

    public void ActualizarUIVida()
    {
        //Debug.Log(vida);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = vida / vidaMaxima;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percent = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percent);
        }
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percent = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percent);
        }
    }

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        lerpTimer = 0f;
        if (vida == 0)
        {
            audioSource.PlayOneShot(muerteS);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().mostrarPantallaFinal();
        }
    }

    public void Curar()
    {
        if (numBotiquines > 0)
        {
            vida = vidaMaxima;
            lerpTimer = 0f;
            numBotiquines--;
            ActualizarHUDBotiquines();
        }
    }

    public void AumentarBotiquin()
    {
        numBotiquines++;
        ActualizarHUDBotiquines();
    }
    
    private void ActualizarHUDBotiquines()
    {
        numeroBotiquinesHUD.text = String.Format("x {0}", numBotiquines);
    }
}
