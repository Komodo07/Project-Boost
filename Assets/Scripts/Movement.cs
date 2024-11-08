using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }    

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput != 0)
        {
            ApplyRotation(rotationInput);
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationThisFrame * rotationSpeed);
        if (rotationThisFrame < 0)
        {
            if (!leftBooster.isPlaying)
            {
                rightBooster.Stop();
                leftBooster.Play();
            }            
        }
        else if (rotationThisFrame > 0)
        {
            if (!rightBooster.isPlaying)
            {
                leftBooster.Stop();
                rightBooster.Play();
            }            
        }
        
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }    
}
