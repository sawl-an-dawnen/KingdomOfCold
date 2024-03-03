using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool debugMode = false;
    
    public int playerHealth = 3;
    public int overDriveShieldBurts = 3;

    public void TakeDamage()
    {
        
        playerHealth -= 1;
       
        if (playerHealth <= 0)
        {
            if (!debugMode)
            {
                Die();
            }
        }
    }

    public void GetSlowed()
    {
        
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.GetComponent<SceneLoader>().LoadScene();
    }
}
