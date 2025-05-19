using Unity.VisualScripting;
using UnityEngine;

public class AK47Script : Armas
{
    protected override void SetTransform()
    {
        Debug.Log("AK47Script SetTransform called");
        transform.localScale = new Vector3(1f, 1f, -1f);
        transform.localPosition = new Vector3(0.195f, -0.113f, 0.337f);
    }
}
