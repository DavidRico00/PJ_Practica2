using UnityEngine;

public abstract class Armas : MonoBehaviour
{
    public int indice;
    public float damage, range;
    protected Camera camara;

    private void Start()
    {
        camara = GetComponentInParent<Camera>();
        SetTransform();
    }

    public void Shoot()
    {
        RaycastHit hit;
        Debug.DrawRay(camara.transform.position, camara.transform.forward * range, Color.red, 1f);
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
        }
    }

    protected virtual void SetTransform(){}
}
