using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin}
    

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    private GameObject spawnedBullet;
    private float timer = 0f;


   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z+1f);
        if(timer >= firingRate)
        {
            Fire();
            timer = 0f;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<KnightBullet>().speed = speed;
            spawnedBullet.GetComponent<KnightBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    
}
