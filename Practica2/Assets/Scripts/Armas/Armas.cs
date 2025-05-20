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
    public AudioClip disparoS, recargaS, noAmmoS;
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

    public void Shoot()
    {
        if (isReloading)
            return;
        if (cargador == 0)
        {
            audioSource.PlayOneShot(noAmmoS);
            return;
        }

        cargador--;
        actualizarHUD();
        RaycastHit hit;
        audioSource.PlayOneShot(disparoS);
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, range, enemigoLayer))
            hit.transform.GetComponent<EnemigoScript>().RecibirDanio(damage);
    }

    private bool isReloading = false;
    public void Reload()
    {
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
}
