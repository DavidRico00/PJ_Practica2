using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform izqArriba, izqAbajo, derArriba, derAbajo;

    void Start()
    {

    }

    void Update()
    {
        
    }
    
    public void SpawnEnemy()
    {
        float minX = Mathf.Min(izqArriba.position.x, izqAbajo.position.x);
        float maxX = Mathf.Max(derArriba.position.x, derAbajo.position.x);
        float minZ = Mathf.Min(izqAbajo.position.z, derAbajo.position.z);
        float maxZ = Mathf.Max(izqArriba.position.z, derArriba.position.z);

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
