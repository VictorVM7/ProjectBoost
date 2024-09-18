using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
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
