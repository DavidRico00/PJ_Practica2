using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OpcionesScript : MonoBehaviour
{
    public Slider sliderBrillo, sliderVolumen, sliderSensibilidadX, sliderSensibilidadY;
    public Image panelBrillo;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        float valor = PlayerPrefs.GetFloat("brillo", 0.5f);
        if (sliderBrillo != null)
            sliderBrillo.value = valor;

        if (sliderVolumen != null)
            sliderVolumen.value = PlayerPrefs.GetFloat("Volumen", -20f);

        if (sliderSensibilidadX != null)
            sliderSensibilidadX.value = PlayerPrefs.GetFloat("SensibilidadX", 30f);
            
        if (sliderSensibilidadY != null)
            sliderSensibilidadY.value = PlayerPrefs.GetFloat("SensibilidadY", 30f);

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }

    public void modificarVolumen(float valor)
    {
        PlayerPrefs.SetFloat("Volumen", valor);
        audioMixer.SetFloat("Volumen", valor);
    }

    public void modificarBrillo(float valor)
    {
        PlayerPrefs.SetFloat("brillo", valor);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }

    public void modificarSensibilidadX(float valor)
    {
        PlayerPrefs.SetFloat("SensibilidadX", valor);

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            p.GetComponent<PlayerController>().CambiarSensibilidad();
    }

    public void modificarSensibilidadY(float valor)
    {
        PlayerPrefs.SetFloat("SensibilidadY", valor);

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            p.GetComponent<PlayerController>().CambiarSensibilidad();
    }
}
