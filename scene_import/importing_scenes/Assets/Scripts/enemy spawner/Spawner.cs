using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableObjects;

    [Tooltip("The Spawner waits a random number of seconds between these two interval each time a object is spawned")]
    [SerializeField] private float minSpawnIntervalInSeconds;
    [SerializeField] private float maxSpawnIntervalInSeconds;

    //initializing player and enemies
    private Death death;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        death = GetComponentInChildren<Death>();
        death.OnReset += DestroyAllSpawnedObjects;

        StartCoroutine(nameof(Spawn));
    }
    
    //spawn enemies and space them our with time
    private IEnumerator Spawn()
    {
        var spawned = Instantiate(GetRandomSpawnableFromList(), transform.position, transform.rotation, transform);
        spawnedObjects.Add(spawned);

        yield return new WaitForSeconds(Random.Range(minSpawnIntervalInSeconds, maxSpawnIntervalInSeconds));
        StartCoroutine(nameof(Spawn));
    }

    //destry spawned objects
    private void DestroyAllSpawnedObjects()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            Destroy(spawnedObjects[i]);
            spawnedObjects.RemoveAt(i);
        }
    }

    // i dont really get the point of this script, i think it jus picks a random spawner to start with
    private GameObject GetRandomSpawnableFromList()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnableObjects.Count);
        return spawnableObjects[randomIndex];
    }
}
