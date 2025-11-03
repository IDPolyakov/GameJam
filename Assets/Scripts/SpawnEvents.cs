using UnityEngine;
using System.Collections.Generic;

public class SpawnEvents : MonoBehaviour
{
    [Header("Объекты для спавна")]
    [SerializeField]
    private GameObject[] prefabsToSpawn;

    [Header("Параметры Спавна")]
    [SerializeField]
    private int initialSpawnCount = 2;
    [SerializeField]
    private int spawnCountChange = 1;

    private BoxCollider spawnArea;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int currentSpawnAmount;

    void Start()
    {
        currentSpawnAmount = initialSpawnCount;
        spawnArea = GetComponent<BoxCollider>();
        spawnArea.enabled = false;
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        Vector3 pos = transform.position + spawnArea.center;
        Vector3 size = new Vector3(transform.localScale.x * spawnArea.size.x, transform.localScale.y * spawnArea.size.y, transform.localScale.z * spawnArea.size.z);
        for (int i = 0; i < currentSpawnAmount; i++)
        {
            float randomX = pos.x + Random.Range(-size.x / 2f, size.x / 2f);
            float randomZ = pos.z + Random.Range(-size.z / 2f, size.z / 2f);

            Vector3 randPos = new Vector3(randomX, transform.position.y, randomZ);

            GameObject prefabToUse = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            spawnedObjects.Add(Instantiate(prefabToUse, randPos, Quaternion.identity));
        }
    }

    private void removeAllSpawnedPrefabs()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Debug.Log(obj.name);
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    public void respawnPrefabs()
    {

        removeAllSpawnedPrefabs();
        currentSpawnAmount = currentSpawnAmount + spawnCountChange;
        SpawnPrefabs();
    }

    public void resetAmount() { currentSpawnAmount = initialSpawnCount - 1; }
}
