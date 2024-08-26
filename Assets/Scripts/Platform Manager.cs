using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platformPrefab;
    public float platformXSize = 9f;
    public float platformZSize = 9f;
    public float platformYSize = 0.2f;

    private GameObject currPlatform;
    private GameObject nextPlatform;
    private bool awaitingSpawn = false;
    public float destroyTime;

    private float minDestroyTime;
    private float maxDestroyTime;
    private float spawnTime;
    void Start()
    {
        StartCoroutine(InitializeVariables());
    }
    IEnumerator InitializeVariables()
    {
        // Wait until the data is loaded
        while (ExtractJson.minPulpitDestroyTime == 0f || ExtractJson.maxPulpitDestroyTime == 0f || ExtractJson.pulpitSpawnTime == 0f)
        {
            yield return null;
        }

        minDestroyTime = ExtractJson.minPulpitDestroyTime;
        maxDestroyTime = ExtractJson.maxPulpitDestroyTime;
        spawnTime = ExtractJson.pulpitSpawnTime;

        destroyTime = Random.Range(minDestroyTime, maxDestroyTime);
        currPlatform = SpawnPlatform(Vector3.zero);
        StartCoroutine(PlatformState(currPlatform));

    }

    // Update is called once per frame
    void Update()
    {
        if (currPlatform != null && !awaitingSpawn)
        {
            StartCoroutine(SpawnNewPlatform());
        }
    }
    GameObject SpawnPlatform(Vector3 position)
    {
        return Instantiate(platformPrefab, position, Quaternion.identity);
    }
    IEnumerator PlatformState(GameObject platform)
    {
        yield return new WaitForSeconds(destroyTime);

        if (platform != null)
        {
            Destroy(platform);
        }
    }
    IEnumerator SpawnNewPlatform()
    {
        awaitingSpawn = true;
        float timeRemaining = destroyTime - Time.timeSinceLevelLoad;
        yield return new WaitForSeconds(timeRemaining - spawnTime);

        if (currPlatform != null)
        {
            Vector3 spawnPosition = NewPosition(currPlatform.transform.position);
            nextPlatform = SpawnPlatform(spawnPosition);

            StartCoroutine(PlatformState(nextPlatform));
        }

        yield return new WaitForSeconds(spawnTime);

        if (nextPlatform != null)
        {
            currPlatform = nextPlatform;
            nextPlatform = null;
            awaitingSpawn = false;
        }
    }

    Vector3 NewPosition(Vector3 currentPos)
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(platformXSize, 0, 0),
            new Vector3(-platformXSize, 0, 0),
            new Vector3(0, 0, platformZSize),
            new Vector3(0, 0, -platformZSize)
        };

        int randomIndex = Random.Range(0, directions.Length);
        return currentPos + directions[randomIndex];
    }


}

