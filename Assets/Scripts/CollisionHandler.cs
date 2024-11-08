using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float sceneLoadDelay;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    AudioSource audioSource;

    bool isControllable = true;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable)        
            return;        

        switch (other.gameObject.tag)
        {
            case "Friendly":            
                Debug.Log("Collided into Friendly object");
                break;
            
            case "Finish":
                StartSuccessSequence();
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
        DisablePlayer();
        audioSource.PlayOneShot(deathSFX);
        deathParticles.Play();
        Invoke(nameof(ReloadScene), sceneLoadDelay);        
    }    

    private void StartSuccessSequence()
    {
        DisablePlayer();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        Invoke(nameof(DetermineFinishProgress), sceneLoadDelay);
    }

    private void DetermineFinishProgress()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadNextScene(nextSceneIndex);            
        }
        else
        {
            GameOver();
        }        
    }

    private void DisablePlayer()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
    }

    private void LoadNextScene(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void GameOver()
    {
        Debug.Log("Congratulations!! You have finished the game!");
        //TODO: Create and event that can be subcribed to by the Movement script
    }
}
