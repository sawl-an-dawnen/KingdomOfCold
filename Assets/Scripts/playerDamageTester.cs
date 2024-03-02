using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamageTester : MonoBehaviour
{
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Pressed " + KeyCode.L);
            // Call the TakeDamage function with a damage amount of 1.
            playerManager.TakeDamage();
        }
    }
}
