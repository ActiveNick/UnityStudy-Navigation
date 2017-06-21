using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour {

    public GameObject prefabBall;
    private SpawnPointManager spawnPointManager;
    private float destroyAfterDelay = 1;

    private float testFireKeyDelay = 0;
    void Start()
    {
        spawnPointManager = GetComponent<SpawnPointManager>();
        StartCoroutine("CheckFireKeyAfterShortDelay");
    }
    IEnumerator CheckFireKeyAfterShortDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(testFireKeyDelay);
            // having waited, now we check every frame
            testFireKeyDelay = 0;
            CheckFireKey();
        }
    }
    private void CheckFireKey()
    {
        if (Input.GetButton("Fire1"))
        {
            CreateSphere();
            // wait half-second before calling next spawn
            testFireKeyDelay = 0.5f;
        }
    }
    private void CreateSphere()
    {
        GameObject spawnPoint = spawnPointManager.NearestSpawnpoint(transform.position);

        if (spawnPoint)
        {
            GameObject newBall = (GameObject) Instantiate(prefabBall,
                spawnPoint.transform.position, Quaternion.identity);
            Destroy(newBall, destroyAfterDelay);
        }
    }
}
