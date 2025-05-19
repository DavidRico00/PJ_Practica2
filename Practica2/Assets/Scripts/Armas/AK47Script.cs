using Unity.VisualScripting;
using UnityEngine;

public class AK47Script : Armas
{
    protected override void SetTransform()
    {
        transform.localPosition = new Vector3(9.59f, 0.9f, -1.58f);
        transform.localScale = new Vector3(1f, 1f, -1f);
    }
}
