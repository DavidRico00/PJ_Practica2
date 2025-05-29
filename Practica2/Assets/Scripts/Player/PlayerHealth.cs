using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private float vida, lerpTimer;
    public float vidaMaxima = 100f, chipSpeed = 2f;
    public Image frontHealthBar, backHealthBar;

    void Start()
    {
        vida = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        vida = Mathf.Clamp(vida, 0f, vidaMaxima);
        ActualizarUIVida();


    }

    public void ActualizarUIVida()
    {
        Debug.Log(vida);
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
        if( fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percent = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percent);
        }
    }

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        lerpTimer = 0f;
    }
    
    public void Curar(float cantidad)
    {
        vida = cantidad;
        lerpTimer = 0f;
    }
}
