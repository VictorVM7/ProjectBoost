using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip landingSound;
    Movement movement;
    AudioSource audioSource;

    void Start() 
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// When player collides with another object
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                FinishSceneSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCreashSequence();
                break;
        }
    }

    /// <summary>
    /// Handle crash event
    /// </summary>
    void StartCreashSequence()
    {
        audioSource.PlayOneShot(collisionSound); 
        movement.enabled = false;
        movement.audioSource.Stop();
        Invoke("ReloadScene", delay);
    }

    /// <summary>
    /// Handle crash event
    /// </summary>
    void FinishSceneSequence()
    {
        audioSource.PlayOneShot(landingSound); 
        movement.enabled = false;
        movement.audioSource.Stop();
        Invoke("LoadNextLevel", delay);
    }

    /// <summary>
    /// Loads next level
    /// </summary>
    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
