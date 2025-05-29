using UnityEngine;
using TMPro;
using System;
using System.Collections;

public abstract class Armas : MonoBehaviour
{
    protected float damage, range;
    protected int cargador, cargadorMax, reservas;
    protected Camera camara;
    private TextMeshProUGUI cargadorHUD;
    private LayerMask enemigoLayer;
    public AudioClip disparoS, recargaS, noAmmoS, hitS;
    private AudioSource audioSource;
    protected Animator animator;

    private void Start()
    {
        cargadorHUD = GameObject.Find("Municion").GetComponent<TextMeshProUGUI>();
        camara = GetComponentInParent<Camera>();
        audioSource = camara.GetComponent<AudioSource>();
        enemigoLayer = LayerMask.GetMask("Enemigo");
        animator = GetComponent<Animator>();

        SetTransform();
        actualizarHUD();
    }

    public int Shoot()
    {
        int p = 0;
        if (isReloading)
            return p;
        if (cargador == 0)
        {
            audioSource.PlayOneShot(noAmmoS);
            return p;
        }

        animator.SetBool("disparo", true);
        cargador--;
        actualizarHUD();
        RaycastHit hit;
        audioSource.PlayOneShot(disparoS);
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, range, enemigoLayer))
        {
            p += 10;
            audioSource.PlayOneShot(hitS);
            if (hit.transform.GetComponent<EnemigoScript>().RecibirDanio(damage)) p += 100;
        }

        return p;
    }

    private bool isReloading = false;
    public void Reload(){
        if (cargador == cargadorMax || reservas <= 0)
            return;

        if (!isReloading)
        {
            isReloading = true;
            StartCoroutine(RecargarDespuesDeSonido());
        }
    }

    private IEnumerator RecargarDespuesDeSonido()
    {
        animator.SetBool("recargando", true);
        audioSource.PlayOneShot(recargaS);

        yield return new WaitForSeconds(recargaS.length);

        int toReload = cargadorMax - cargador;
        if (reservas < toReload)
            toReload = reservas;

        cargador += toReload;
        reservas -= toReload;
        isReloading = false;
        actualizarHUD();
        animator.SetBool("recargando", false);
    }

    public void SumarMunicion(int cantidad)
    {
        reservas += cantidad;
        if (reservas > 99)
            reservas = 99;
        actualizarHUD();
    }

    protected virtual void SetTransform() { }

    protected void actualizarHUD()
    {
        cargadorHUD.text = String.Format("{0}/{1}", cargador, reservas);
    }


    protected bool apuntando;
    public void Apuntar()
    {
        animator.SetBool("meteMira", true);
        apuntando = true;
    }

    public void Desapuntar()
    {
        animator.SetBool("meteMira", false);
        apuntando = false;
    }

    public void setApuntando()
    {
        animator.SetBool("apuntando", apuntando);
        if (apuntando)
        {

        }
        else
        {

        }


    }


    public void setDisparoFalse()
    {
        animator.SetBool("disparo", false);
    }
}
