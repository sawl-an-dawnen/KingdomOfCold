using TMPro;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{

    public string[] instructions;
    private playerMovement movementScript;
    [SerializeField] private TextMeshProUGUI prompt;
    private int i = 0;
    

    // Update is called once per frame
    void Update()
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();

        if (i == 0) {
            prompt.text = instructions[i];
            if (movementScript.moveVector.magnitude > 0)
            {
                Debug.Log("You moved!");
                i++;
            }
            
        }
        else if (i == 1) {
            prompt.text = instructions[i];
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                Debug.Log("You Dodged!");
                i++;
            }
        }
        else if (i == 2)
        {
            prompt.text = instructions[i];
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Debug.Log("Oh shit!");
                i++;
            }
        }
        else
        {
            prompt.text = " ";
        }


        
        
     
        
        
    }
        
}

