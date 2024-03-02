using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    
    public int playerHealth = 3;
    public int overDriveShieldBurts = 3;

    public void TakeDamage()
    {
        
        playerHealth -= 1;
       
        if (playerHealth <= 0)
        {
            Die();
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
