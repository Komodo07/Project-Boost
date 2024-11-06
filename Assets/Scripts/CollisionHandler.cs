using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;

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
                int nextSceneIndex = currentSceneIndex + 1;
                if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    LoadNextScene(nextSceneIndex);
                    break;
                }
                else
                {
                    GameOver();
                    break;
                }
            
            case "Fuel":            
                Debug.Log("Collided into Fuel object");
                break;
            
            default:
                ReloadScene();                
                break;
            
        }
    }

    private void GameOver()
    {
        Debug.Log("Congratulations!! You have finished the game!");
        //TODO: Create and event that can be subcribed to by the Movement script
    }

    private void LoadNextScene(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
