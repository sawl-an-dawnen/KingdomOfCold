using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public int playerHealth = 3;
    public int playerMaxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        
        playerHealth -= damageAmount;
       
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void GetSlowed(float slowedAmount)
    {

    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
