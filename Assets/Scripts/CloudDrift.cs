using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CloudDrift : MonoBehaviour
{
    public float speed = 2f; // Adjust this value to control the speed of movement
    public float distance = 10f;
    public float directionSwap = 1f;
    private float timer=0f;

    private Vector2 newLocation;

    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            newLocation = MoveCloud();
            timer = directionSwap;
        }
        
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newLocation, step);
    }

    Vector2 MoveCloud()
    {
        // Get the current position of the cloud
        Vector2 currentPosition = transform.position;

        // Generate a random direction for movement
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Calculate the new position based on the random direction and speed
        Vector2 newPosition = currentPosition + randomDirection * distance;

        // Clamp the new position to ensure the cloud stays within the camera's view
        //newPosition.x = Mathf.Clamp(newPosition.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
        //newPosition.y = Mathf.Clamp(newPosition.y, Camera.main.ViewportToWorldPoint(Vector3.zero).y, Camera.main.ViewportToWorldPoint(Vector3.one).y);

        // Update the position of the cloud
        return newPosition;
    }
}
