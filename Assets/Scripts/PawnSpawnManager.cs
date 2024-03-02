using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnSpawnManager : MonoBehaviour
{
    public GameObject[] _spawnPawn; //pawn instance
    public GameObject _spawnPawnDamage; //pawn instance
    public GameObject _spawnPawnTimer; //pawn instance

    public float spawnRate = 5.0f; // rate at which pawns spawns
    public float variance = 3.0f;

    private float currSpawnTimer;

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
            
            currSpawnTimer = spawnRate + Random.Range(-variance,variance);
        }
    }
}
