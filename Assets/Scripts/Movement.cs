using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, set in the editor

    // CACHE - references for readability or speed

    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    public AudioSource audioSource { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward, rotationThrust);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back, rotationThrust);
        }
    }

    public void ApplyRotation(Vector3 vector3, float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freeze rotation so we can manually rotate
        transform.Rotate(vector3 * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreeze rotation so physics can take control
    }
}
