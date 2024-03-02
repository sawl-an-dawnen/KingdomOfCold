using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private bool isGameActive = true;
    [SerializeField] public GameObject _spawnPawnPrefab; //pawn instance

    public float xSpawnRange = 5.0f; //X-position pawn spawning bounds
    public float ySpawn = 0.0f; //Y-position pawn spawn
    public float currSpawnTimer = 0.0f;
    public float spawnRate = 5.0f; // rate at which pawns spawns
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (isGameActive)
        {
            if (currSpawnTimer <= Time.time)
            {
                currSpawnTimer = Time.time + spawnRate + (Time.time - currSpawnTimer);
                Instantiate(_spawnPawnPrefab,
                new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn, 0), Quaternion.identity);
            }
        }
    }

    /*IEnumerator SpawnRoutine()
    {

        while (isGameActive)
        {
            Instantiate(_spawnPawnPrefab, 
                new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnRateTime);


        }
    }
    */
}
