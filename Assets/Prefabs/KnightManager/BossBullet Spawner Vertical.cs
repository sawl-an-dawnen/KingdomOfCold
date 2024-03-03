using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VerticalSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, SpinBackWard}
    

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    private GameObject spawnedBullet;
    private float timer = 0f;


   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if(spawnerType == SpawnerType.SpinBackWard) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if(timer >= firingRate)
        {
            if(spawnerType == SpawnerType.SpinBackWard)
            {
                FireBackward();
                timer = 0f;
            }
            else 
            {
                Fire();
                timer = 0f;
            }

        }
        
        
        
        /*if(timer >= firingRate && spawnerType != SpawnerType.Test)
        {
            Fire();
            timer = 0f;
        }
        else if(timer >= firingRate && spawnerType == SpawnerType.Test)
        {
            
            FireBackward();
            timer = 0f;
        }*/
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<BossBullet>().speed = speed;
            spawnedBullet.GetComponent<BossBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;

        }
    }

    private void FireBackward()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<BossBullet>().speed = -speed;
            spawnedBullet.GetComponent<BossBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    private void FireUp()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<BossBullet>().speed =  speed ;
            spawnedBullet.GetComponent<BossBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }


}
