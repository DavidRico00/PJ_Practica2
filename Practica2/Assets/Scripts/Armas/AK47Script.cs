using UnityEngine;

public class AK47Script : Armas
{
    private void Awake()
    {
        damage = 25f;
        range = 100f;
        cargadorMax = 20;
        cargador = cargadorMax;
        reservas = cargadorMax * 3;

        apuntando = false;
    }

    protected override void SetTransform()
    {
        transform.localScale = new Vector3(1f, 1f, -1f);
        transform.localPosition = new Vector3(0.195f, -0.113f, 0.337f);
    }

}
