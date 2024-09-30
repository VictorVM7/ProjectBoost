using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip landingSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// When player collides with another object
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(collisionSound); 
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }

    /// <summary>
    /// Handle crash event
    /// </summary>
    void FinishSceneSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(landingSound); 
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
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
