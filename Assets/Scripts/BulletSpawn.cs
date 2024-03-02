using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private bool isPawnActive = true;
    [SerializeField] public GameObject _spawnBulletPrefab; //pawn instance

    public float xSpawn = 5.0f; //X-position pawn spawning bounds
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
        while (isPawnActive)
        {
            if (currSpawnTimer <= Time.time)
            {
                currSpawnTimer = Time.time + spawnRate + (Time.time - currSpawnTimer);
                Instantiate(_spawnBulletPrefab, new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
            }
        }
    }
}
