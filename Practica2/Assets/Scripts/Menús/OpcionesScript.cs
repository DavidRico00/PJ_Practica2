using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OpcionesScript : MonoBehaviour
{
    public Slider sliderBrillo;
    public Image panelBrillo;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        float valor = PlayerPrefs.GetFloat("brillo", 0.5f);

        if (sliderBrillo != null)
            sliderBrillo.value = valor;

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }

    public void modificarSlider(float valor)
    {

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
    }

    public void modificarSensibilidadY(float valor)
    {
        PlayerPrefs.SetFloat("SensibilidadY", valor);
    }
}
