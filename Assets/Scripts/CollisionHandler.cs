using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float sceneLoadDelay;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":            
                Debug.Log("Collided into Friendly object");
                break;
            
            case "Finish":
                GetComponent<Movement>().enabled = false;
                Invoke("LoadNextScene", sceneLoadDelay);
                break;
            
            case "Fuel":            
                Debug.Log("Collided into Fuel object");
                break;
            
            default:
                StartCrashSequence();      
                break;
            
        }
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", sceneLoadDelay);        
    }

    private void GameOver()
    {
        Debug.Log("Congratulations!! You have finished the game!");
        //TODO: Create and event that can be subcribed to by the Movement script
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {            
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            GameOver();
        }        
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
