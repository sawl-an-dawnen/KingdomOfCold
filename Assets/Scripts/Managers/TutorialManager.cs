using TMPro;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{

    public string[] instructions;
    public float promptTimer = 8f;
    private float timer;
    private playerMovement movementScript;
    public TextMeshProUGUI prompt;
    private int i = 0;


    void Start() 
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        timer = promptTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (i == 2)
        {
            prompt.text = instructions[i];
            if (movementScript.moveVector.magnitude > 0)
            {
                Debug.Log("You moved!");
                i++;
                timer = promptTimer;
            }
        }
        else if (i == 4)
        {
            prompt.text = instructions[i];
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                Debug.Log("You Dodged!");
                i++;
                timer = promptTimer;
            }
        }
        else if (i == 6)
        {
            prompt.text = instructions[i];
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Debug.Log("Oh shit!");
                i++;
                timer = promptTimer;
            }
        }
        else if (i == 12) 
        {
            gameObject.GetComponent<SceneLoader>().LoadScene();
        }
        else
        {
            prompt.text = instructions[i];
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                i++;
                timer = promptTimer;
            }
        } 
    }
        
}

