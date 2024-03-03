using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBulletSpawnerHorizantal : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, SpinBackWard, StraigtUp }
    

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
        if (spawnerType == SpawnerType.StraigtUp) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
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
            spawnedBullet.GetComponent<BossBullet>().speed = 0; // No speed along the x-axis
            spawnedBullet.GetComponent<BossBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Rotate to face upwards
            StartCoroutine(MoveUp(spawnedBullet.transform));
        }
    }

    private IEnumerator MoveUp(Transform bulletTransform)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = bulletTransform.position;
        Vector3 targetPosition = startPosition + Vector3.up; // Move 1 unit upwards

        while (elapsedTime < bulletLife)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / bulletLife;
            bulletTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        Destroy(bulletTransform.gameObject); // Destroy the bullet after its lifetime
    }


}
