using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string level;
    public GameObject[] activateArray;
    public bool isReset = false;

    public void LoadScene() {
        foreach (GameObject obj in activateArray) 
        {
            obj.SetActive(true);
        }
        if (isReset) 
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }
        SceneManager.LoadScene(level);
    }
    public void CloseGame() {
        Application.Quit();
    }
}



