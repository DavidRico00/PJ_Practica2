using UnityEngine;

public class NoDestruir : MonoBehaviour
{
    private void Awake()
    {
        var objs = FindObjectsByType<NoDestruir>(FindObjectsSortMode.None);
        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
