using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnSpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject[] _spawnPawn; //pawn instance
    [SerializeField] public GameObject _spawnPawnDamage; //pawn instance
    [SerializeField] public GameObject _spawnPawnTimer; //pawn instance
    [SerializeField] public GameObject _spawnPoint; //pawn instance

    //public float xSpawn = 5.0f; //X-position pawn spawning bounds
    //public float ySpawn = 0.0f; //Y-position pawn spawn
    private float currSpawnTimer;
    public float spawnRate = 5.0f; // rate at which pawns spawns
    
    // Start is called before the first frame update
    void Start()
    {
        currSpawnTimer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //int pawnSpawnType = Random.Range(0, 2);
        currSpawnTimer -= Time.deltaTime;
        //Debug.Log(Random.Range(0, 2));
        if (currSpawnTimer <= 0.0f)
        {

            //currSpawnTimer = Time.deltaTime + spawnRate + (Time.deltaTime - currSpawnTimer);

            int pawnSpawnType = Random.Range(0, 2);

            if (pawnSpawnType == 0 )
            {
                // Spawn Pawn at SpawnPoint
                Instantiate(_spawnPawnDamage, gameObject.transform.position, this.transform.rotation, gameObject.transform);
                _spawnPawnDamage.GetComponent<SpriteRenderer>().enabled = true;
                //Debug.Log(pawnSpawnType);
                //Debug.Log("Spawn Damage Pawn");          
            }
            if (pawnSpawnType == 1 )
            {
                // Spawn Pawn at SpawnPoint
                Instantiate(_spawnPawnTimer, gameObject.transform.position, this.transform.rotation, gameObject.transform);
                _spawnPawnTimer.GetComponent<SpriteRenderer>().enabled = true;
                //Debug.Log(pawnSpawnType);
                //Debug.Log("Spawn Timer Pawn");
            }

            // Spawn Pawn Prefab at random x.position within set bounds
            //Instantiate(_spawnPawnPrefab,
            //new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn, 0), Quaternion.identity);

            
            currSpawnTimer = spawnRate;
            

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
