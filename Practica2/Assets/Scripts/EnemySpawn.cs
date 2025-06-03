using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform izqArriba, izqAbajo, derArriba, derAbajo;

    public float spawnInterval;
    private float timer = 0f;
    public int enemyCount;
    public float percent;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            enemyCount = Mathf.CeilToInt(enemyCount * percent);
            SpawnEnemy(enemyCount);
            timer = 0f;
        }
    }
    
    public void SpawnEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float minX = Mathf.Min(izqArriba.position.x, izqAbajo.position.x);
            float maxX = Mathf.Max(derArriba.position.x, derAbajo.position.x);
            float minZ = Mathf.Min(izqAbajo.position.z, derAbajo.position.z);
            float maxZ = Mathf.Max(izqArriba.position.z, derArriba.position.z);

            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemy.GetComponent<EnemigoScript>().refugio = transform.Find("Refugio").gameObject;
        }
    }
}
