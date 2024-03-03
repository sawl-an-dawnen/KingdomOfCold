using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerStateChange : MonoBehaviour
{

    public TransformTransition spawner;

    public void OnTriggerEnter2D(Collider2D other)
    {
        spawner.state = !spawner.state;
    }
}
