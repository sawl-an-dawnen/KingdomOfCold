using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] public GameObject _spawnBulletDamage; //pawn instance
    [SerializeField] public GameObject _spawnBulletTime; //pawn instance
    public float spawnRate = 5.0f; // rate at which pawns spawns

    private float currSpawnTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currSpawnTimer -= Time.deltaTime;

        if (currSpawnTimer <= 0f)
        {
            int bulletType = Random.Range(0, 1);
            if (bulletType == 0)
            {
                Instantiate(_spawnBulletDamage, gameObject.transform.position, this.transform.rotation, gameObject.transform);
            }
            if (bulletType == 1)
            {
                Instantiate(_spawnBulletTime, gameObject.transform.position, this.transform.rotation, gameObject.transform);
            }
            currSpawnTimer = spawnRate;
        }
    }
}